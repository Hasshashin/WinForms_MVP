﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WinFormsMvp.Binder
{
    /// <summary>
    /// A strategy for discovery presenters based on [PresenterBinding] attributes being placed
    /// on views and view hosts.
    /// </summary>
    public class AttributeBasedPresenterDiscoveryStrategy : IPresenterDiscoveryStrategy
    {
        static readonly IDictionary<RuntimeTypeHandle, IEnumerable<PresenterBindingAttribute>> typeToAttributeCache
            = new Dictionary<RuntimeTypeHandle, IEnumerable<PresenterBindingAttribute>>();

        /// <summary>
        /// Gets the presenter bindings for passed views.
        /// </summary>
        /// <param name="viewInstance">The view instance (user control or form).</param>
        public PresenterDiscoveryResult GetBinding(IView viewInstance) 
        {
            if (viewInstance == null)
                throw new ArgumentNullException("viewInstance");

            var messages = new List<string>();
            var bindings = new List<PresenterBinding>();

            var viewType = viewInstance.GetType();

            var viewDefinedAttributes = GetAttributes(typeToAttributeCache, viewType);

            if (viewDefinedAttributes.Empty())
            {
                messages.Add(string.Format(
                    CultureInfo.InvariantCulture,
                    "could not find a [PresenterBinding] attribute on view instance {0}",
                    viewType.FullName
                                 ));
            }
            
            if (viewDefinedAttributes.Any())
            {
                foreach (var attribute in viewDefinedAttributes.OrderBy(a => a.PresenterType.Name))
                {
                    if (!attribute.ViewType.IsAssignableFrom(viewType))
                    {
                        messages.Add(string.Format(
                            CultureInfo.InvariantCulture,
                            "found, but ignored, a [PresenterBinding] attribute on view instance {0} (presenter type: {1}, view type: {2}, binding mode: {3}) because the view type on the attribute is not compatible with the type of the view instance",
                            viewType.FullName,
                            attribute.PresenterType.FullName,
                            attribute.ViewType.FullName,
                            attribute.BindingMode
                                         ));
                        continue;
                    }

                    messages.Add(string.Format(
                        CultureInfo.InvariantCulture,
                        "found a [PresenterBinding] attribute on view instance {0} (presenter type: {1}, view type: {2}, binding mode: {3})",
                        viewType.FullName,
                        attribute.PresenterType.FullName,
                        attribute.ViewType.FullName,
                        attribute.BindingMode
                                     ));

                    bindings.Add(new PresenterBinding(
                                     attribute.PresenterType,
                                     attribute.ViewType,
                                     attribute.BindingMode,
                                     viewInstance
                                     ));
                }
            }
            else
            {
                return null;
            }

            return new PresenterDiscoveryResult(
                new [] { bindings.Single().ViewInstance },
                "AttributeBasedPresenterDiscoveryStrategy:\r\n" +
                string.Join("\r\n", messages.Select(m => "- " + m).ToArray()),
                bindings
                );

        }

        /********************  Not used ********************/  
        static IEnumerable<IView> GetViewInstancesToBind(IEnumerable<IView> pendingViewInstances, IView viewInstance, Type viewType, ICollection<string> messages, PresenterBindingAttribute attribute)
        {
            IEnumerable<IView> viewInstancesToBind;
            switch (attribute.BindingMode)
            {
                case BindingMode.Default:
                    viewInstancesToBind = new[] { viewInstance };
                    break;
                case BindingMode.SharedPresenter:
                    viewInstancesToBind = pendingViewInstances
                        .Where(v => attribute.ViewType.IsAssignableFrom(viewType))
                        .ToArray();

                    messages.Add(string.Format(
                        CultureInfo.InvariantCulture,
                        "including {0} more view instances in the binding because the binding mode is {1} and they are compatible with the view type {2}",
                        viewInstancesToBind.Count() - 1,
                        attribute.BindingMode,
                        attribute.ViewType.FullName
                    ));

                    break;
                default:
                    throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Binding mode {0} is not supported", attribute.BindingMode));
            }
            return viewInstancesToBind;
        }

        /// <summary>
        /// Gets the custom attributes for the Type passed in as the 2nd parameter.
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="sourceType">The Type for which custom attributes have been added.</param>
        /// <returns>An IEnumerable of PresenterBindingAttribute</returns>
        internal static IEnumerable<PresenterBindingAttribute> GetAttributes(IDictionary<RuntimeTypeHandle, IEnumerable<PresenterBindingAttribute>> cache, Type sourceType)
        {
            var hostTypeHandle = sourceType.TypeHandle;

            //  Todo: look at whether we need the extension method call here. If PresenterBinder is a member of the View, this is probably not necessary.
            return cache.GetOrCreateValue(hostTypeHandle, () =>
            {
                var attributes = sourceType
                    .GetCustomAttributes(typeof(PresenterBindingAttribute), true)
                    .OfType<PresenterBindingAttribute>()
                    .ToArray();

                if (attributes.Any(a =>
                        a.BindingMode == BindingMode.SharedPresenter &&
                        a.ViewType == null
                    ))
                {
                    throw new NotSupportedException(string.Format(
                        CultureInfo.InvariantCulture,
                        "When a {1} is applied with BindingMode={2}, the ViewType must be explicitly specified. One of the bindings on {0} violates this restriction.",
                        sourceType.FullName,
                        typeof(PresenterBindingAttribute).Name,
                        Enum.GetName(typeof(BindingMode), BindingMode.SharedPresenter)
                    ));
                }

                attributes = attributes
                    .Select(pba =>
                        new PresenterBindingAttribute(pba.PresenterType)
                        {
                            ViewType = pba.ViewType ?? sourceType,
                            BindingMode = pba.BindingMode
                        })
                    .ToArray();

                return attributes;
            });
        }
    }
}