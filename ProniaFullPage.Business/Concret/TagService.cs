using ProniaFullPage.Business.Abstract;
using ProniaFullPage.Business.Exceptions;
using ProniaFullPage.Core.Models;
using ProniaFullPage.Core.RepositoryAbstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaFullPage.Business.Concret
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task AddAsyncTag(Tag tag)
        {
            if(! _tagRepository.GetAll().Any(x=>x.Name==tag.Name))
                await _tagRepository.AddAsync(tag);

            else
            {
                throw new DublicateException("Eyni adli tag ola bilmez!");
            }

            await _tagRepository.CommitAsync();
        }

        public void DeleteTag(int id)
        {
           var exsist= _tagRepository.Get(x=>x.Id==id);

            if (exsist == null) throw new NullReferenceException();

            _tagRepository.Delete(exsist);
            _tagRepository.Commit();
        }

        public List<Tag> GetAllTags(Func<Tag, bool>? func = null)
        {
            return _tagRepository.GetAll(func);
        }

        public Tag GetTag(Func<Tag, bool>? func = null)
        {
            return _tagRepository.Get(func);
        }

        public void UpdateTag(int id, Tag tag)
        {
           var oldTag= _tagRepository.Get(x=> x.Id==id);    
            if(oldTag == null) throw new NullReferenceException();

            if(!_tagRepository.GetAll().Any(x=> x.Name==tag.Name))
            {
                oldTag.Name = tag.Name;
            }
            else
            {
                throw new DublicateException("Eyni adli Tag ola bilmez!");
            }

            _tagRepository.Commit();
        }

    }
}
