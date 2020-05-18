using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;

namespace Avolantis.AspNetCore.AzureFunctionsHost.Server
{
    public class FunctionsServerFeatureCollection: IFeatureCollection
    {
        private readonly IFeatureCollection _featureCollectionImplementation;

        public FunctionsServerFeatureCollection()
        {
            _featureCollectionImplementation = new FeatureCollection
            {
                [typeof(IServerAddressesFeature)] = new FunctionsServerAddressesFeature()
            };
        }

        public IEnumerator<KeyValuePair<Type, object>> GetEnumerator()
        {
            return _featureCollectionImplementation.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _featureCollectionImplementation).GetEnumerator();
        }

        public TFeature Get<TFeature>()
        {
            return _featureCollectionImplementation.Get<TFeature>();
        }

        public void Set<TFeature>(TFeature instance)
        {
            _featureCollectionImplementation.Set(instance);
        }

        public bool IsReadOnly => _featureCollectionImplementation.IsReadOnly;

        public object this[Type key]
        {
            get => _featureCollectionImplementation[key];
            set => _featureCollectionImplementation[key] = value;
        }

        public int Revision => _featureCollectionImplementation.Revision;
    }
}