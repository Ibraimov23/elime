using ElimeProject.Models;
using System.Collections.Generic;

namespace ElimeProject.ViewModels
{
    public class UsersListViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public PaginatedListViewModel<User> PageViewModel { get; set; }
    }
}
