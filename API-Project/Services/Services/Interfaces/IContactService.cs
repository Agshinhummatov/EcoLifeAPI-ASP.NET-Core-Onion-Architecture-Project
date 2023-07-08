using Services.DTOs.Advertising;
using Services.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IContactService
    {

        Task<IEnumerable<ContactListDto>> GetAllAsync();
        Task<ContactListDto> GetByIdAsync(int? id);
        Task CreateAsync(ContactCreateDto contactCreateDto);
        Task DeleteAsync(int? id);
       
        Task<IEnumerable<ContactListDto>> SearchAsync(string? searchText);
        Task SoftDeleteAsync(int? id);
    }
}
