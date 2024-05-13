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
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }
        public async Task AddAsyncSlider(Slider slider)
        {
            if(slider.ImageFile.ContentType != "image/png" &&  slider.ImageFile.ContentType != "image/jpeg")
            {
                throw new ImageContentTypeException("Fayl Formati movcud deyil!");
            }
            if (slider.ImageFile.Length > 2097152)
            {
                throw new ImageSizeException("Fayl olcusu coxdur!");
            }

            string fileName= Guid.NewGuid().ToString()+Path.GetExtension(slider.ImageFile.FileName);

            string path = "C:\\Users\\V&V\\Desktop\\CodeAcademy\\ProniaFullPage\\ProniaFullPage\\wwwroot\\" + "uploads\\sliders\\" + fileName;

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                slider.ImageFile.CopyTo(fileStream);
            }
            slider.ImageURL = fileName;

                await _sliderRepository.AddAsync(slider);
               await _sliderRepository.CommitAsync();
        }

        public void DeleteSlider(int id)
        {
           var exsist= _sliderRepository.Get(x=> x.Id == id);
            if (exsist == null) throw new NullReferenceException();

            _sliderRepository.Delete(exsist);
            _sliderRepository.Commit();
        }

        public List<Slider> GetAllSliders(Func<Slider, bool>? func = null)
        {
            return _sliderRepository.GetAll(func);
        }

        public Slider GetSlider(Func<Slider, bool>? func = null)
        {
            return _sliderRepository.Get(func);
        }

        public void UpdateSlider(int id, Slider newSlider)
        {
            var oldSlider= _sliderRepository.Get(x=> x.Id == id);

            if(oldSlider == null) throw new NullReferenceException();

            if(! _sliderRepository.GetAll().Any(x=> x.Title == newSlider.Title))
            {
                oldSlider.Title = newSlider.Title;
                oldSlider.Description = newSlider.Description;
                oldSlider.RedirectUrl = newSlider.RedirectUrl;
                oldSlider.ImageFile= newSlider.ImageFile;   
                oldSlider.ImageURL= newSlider.ImageURL;

            }
            _sliderRepository.Commit();
        }
    }
}
