﻿using _01_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IFileUploader _fileUploader;

        public SlideApplication(ISlideRepository slideRepository, IFileUploader fileUploader)
        {
            _slideRepository = slideRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();
            var pictureName =_fileUploader.Upload(command.Picture, "Slides");
            var slide = new Slide(pictureName, command.PictureAlt,
                command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText, command.Link);
            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(command.Id);
            if (slide is null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }
            var pictureName = _fileUploader.Upload(command.Picture, "Slides");
            slide.Edit(pictureName, command.PictureAlt,
                command.PictureTitle, command.Heading,
                command.Title, command.Text, command.BtnText,command.Link);
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide is null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }
           
            slide.Remove();
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide is null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }

            slide.Restore();
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }

        public List<SlideViewModel> GetList()
        {
            return _slideRepository.GetList();
        }
    }
}
