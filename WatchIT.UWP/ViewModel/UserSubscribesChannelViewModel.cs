using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WatchIT.Domain.Model;

namespace WatchIT.UWP.ViewModel
{
    public class UserSubscribesChannelViewModel
    {
        public ObservableCollection<UserSubscribesChannel> UserSubscribesChannels { get; set; }
        public UserSubscribesChannel UserSubscribesChannel { get; set; }
        public UserSubscribesChannelViewModel()
        {

            UserSubscribesChannels = new ObservableCollection<UserSubscribesChannel>();
            UserSubscribesChannel = new UserSubscribesChannel();
        }
        internal async Task InsertAsync()
        {
            await App.UnitOfWork.UserSubscribesChannelRepository.CreateAsync(UserSubscribesChannel);
        }

        internal async Task DeleteAsync(UserSubscribesChannel userSubscribesChannel)
        {
            await App.UnitOfWork.UserSubscribesChannelRepository.DeleteAsync(userSubscribesChannel);
            UserSubscribesChannels.Remove(userSubscribesChannel);

        }
    }

}
