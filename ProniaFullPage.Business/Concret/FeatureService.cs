using ProniaFullPage.Business.Abstract;
using ProniaFullPage.Business.Exceptions;
using ProniaFullPage.Core.Models;
using ProniaFullPage.Core.RepositoryAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaFullPage.Business.Concret
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;

        public FeatureService(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }
        public async Task AddAsyncFeature(Feature feature)
        {
            if(!_featureRepository.GetAll().Any(x=>x.Title==feature.Title))
               await _featureRepository.AddAsync(feature);
            else
            {
                throw new DublicateException("Eyni title ola bilmez!");
            }

            await _featureRepository.CommitAsync();
        }

        public void DeleteFeature(int id)
        {
           var exsist= _featureRepository.Get(x=>x.Id==id);
            if (exsist == null) throw new NullReferenceException("Id movcud deyil!");

            _featureRepository.Delete(exsist);
            _featureRepository.Commit();
        }

        public List<Feature> GetAllFeatures(Func<Feature, bool>? func = null)
        {
           return _featureRepository.GetAll(func);
        }

        public Feature GetFeature(Func<Feature, bool>? func = null)
        {
            return _featureRepository.Get(func);
        }

        public void UpdateFeature(int id, Feature feature)
        {
            var oldFeature=_featureRepository.Get(x=> x.Id==id);

            if(oldFeature == null) throw new NullReferenceException("Id movcud deyl!");

            if(!_featureRepository.GetAll().Any(x => x.Title == feature.Title))
            {
                oldFeature.Icon = feature.Icon;
                oldFeature.Title = feature.Title;
                oldFeature.Description = feature.Description;
            }

            _featureRepository.Commit();
        }
    }
}
