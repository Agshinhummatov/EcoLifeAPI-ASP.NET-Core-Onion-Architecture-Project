using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.Advertising;
using Services.DTOs.Contact;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ContactService :IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository contactRepository, IMapper mapper)
        {
           
            _mapper = mapper;
            _contactRepository = contactRepository;
        }


        public async Task CreateAsync(ContactCreateDto contactCreateDto)
        {

            var validationContext = new ValidationContext(contactCreateDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(contactCreateDto, validationContext, validationResults, true);

            if (!isValid)
            {
                string errorMessages = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new Exception(errorMessages);
            }

            if (string.IsNullOrEmpty(contactCreateDto.Email) || string.IsNullOrEmpty(contactCreateDto.Content))
            {
                throw new Exception("Email and Content are required.");
            }

            if (string.IsNullOrEmpty(contactCreateDto.Subject) || string.IsNullOrEmpty(contactCreateDto.Name))
            {
                throw new Exception("Subject and Description are required.");
            }

            var mapContact = _mapper.Map<Contact>(contactCreateDto);
          
            await _contactRepository.CreateAsync(mapContact);


        }

        public async Task<IEnumerable<ContactListDto>> GetAllAsync() => _mapper.Map<IEnumerable<ContactListDto>>(await _contactRepository.FindAllAsync());

        public async Task<ContactListDto> GetByIdAsync(int? id) => _mapper.Map<ContactListDto>(await _contactRepository.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _contactRepository.DeleteAsync(await _contactRepository.GetByIdAsync(id));



        public async Task<IEnumerable<ContactListDto>> SearchAsync(string? searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return _mapper.Map<IEnumerable<ContactListDto>>(await _contactRepository.FindAllAsync());
            return _mapper.Map<IEnumerable<ContactListDto>>(await _contactRepository.FindAllAsync(m => m.Name.Contains(searchText)));
        }

        public async Task SoftDeleteAsync(int? id)
        {
            await _contactRepository.SoftDeleteAsync(id);
        }

    }
}
