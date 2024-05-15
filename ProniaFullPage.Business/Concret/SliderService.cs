using Microsoft.AspNetCore.Hosting;
using ProniaFullPage.Business.Abstract;
using ProniaFullPage.Business.Exceptions;
using ProniaFullPage.Core.Models;
using ProniaFullPage.Core.RepositoryAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaFullPage.Business.Concret;

public class SliderService : ISliderService
{
    private readonly ISliderRepository _sliderRepository;
    private readonly IWebHostEnvironment _env;
    public SliderService(ISliderRepository sliderRepository, IWebHostEnvironment env)
    {
        _sliderRepository = sliderRepository;
        _env = env;
    }


    public async Task AddAsyncSlider(Slider slider)
    {
        if (slider.ImageFile == null) throw new FileNullReferanceException("Fayl bos ola bilmez!");


        if(slider.ImageFile.ContentType != "image/png" &&  slider.ImageFile.ContentType != "image/jpeg")
        {
            throw new ImageContentTypeException("Fayl Formati movcud deyil!");
        }
        if (slider.ImageFile.Length > 2097152)
        {
            throw new ImageSizeException("Fayl olcusu coxdur!");
        }

        string fileName= Guid.NewGuid().ToString()+Path.GetExtension(slider.ImageFile.FileName);

        string path = _env.WebRootPath + "\\uploads\\sliders\\" + fileName;

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
        if (exsist == null) throw new NotFoundIdException("Not Found!");

        string path= _env.WebRootPath + "\\uploads\\sliders\\" + exsist.ImageURL;

        if (!File.Exists(path)) throw new NotFoundFileException("File movcud deyil");

        File.Delete(path);

        _sliderRepository.Delete(exsist);
        _sliderRepository.Commit();


        
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

        if(oldSlider == null) throw new NullReferenceException("Id movcud deyil!");

        if(newSlider.ImageFile != null)
        {
            if (newSlider.ImageFile.ContentType != "image/png" && newSlider.ImageFile.ContentType != "image/jpeg")
            {
                throw new ImageContentTypeException("Format duzgun deyl!");
            }
            if (newSlider.ImageFile.Length > 2097152)
            {
                throw new ImageSizeException("Sekil olcusu 2mb cox ola bilmez!");
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(newSlider.ImageFile.FileName);

            string path = _env.WebRootPath + "\\uploads\\sliders\\" + fileName;

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                newSlider.ImageFile.CopyTo(fileStream);
            }
            string oldPath= _env.WebRootPath + "\\uploads\\sliders\\" + oldSlider.ImageURL;
            if(!File.Exists(oldPath))
                throw new FileNotFoundException("File movcud deyil!");

            File.Delete(oldPath);

            oldSlider.ImageURL = fileName;
        }
       
        oldSlider.Title = newSlider.Title;
        oldSlider.Description = newSlider.Description;
        oldSlider.RedirectUrl = newSlider.RedirectUrl;
        
        _sliderRepository.Commit();
    }


}
