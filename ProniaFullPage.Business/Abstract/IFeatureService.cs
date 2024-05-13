using ProniaFullPage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaFullPage.Business.Abstract
{
    public interface IFeatureService
    {
        Task AddAsyncFeature(Feature feature);

        void DeleteFeature(int id);

        void UpdateFeature(int id, Feature feature);

        Feature GetFeature(Func<Feature, bool>? func = null);

        List<Feature> GetAllFeatures(Func<Feature, bool>? func = null);
    }
}
