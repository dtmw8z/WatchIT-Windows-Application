using Microsoft.EntityFrameworkCore;
using System;
using WatchIT.Domain;
using WatchIT.Domain.Model;
using WatchIT.Infrastructure;

namespace WatchITConsole
{
    class Program
    {
        static string SqlConnectionString = "Server=localhost; Initial Catalog=watch-it; Integrated Security = True;";
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            DbContextOptionsBuilder<WatchitDbContext> builder = new DbContextOptionsBuilder<WatchitDbContext>();
            builder.UseSqlServer(SqlConnectionString);



            IUnitOfWork UOW = new UnitOfWork(builder.Options);

            //User usr1 = new User();
            //usr1.fullName = "Susan";
            //usr1.Email = "dtmw8z.sp@gmail.com";
            //usr1.Password = "Heyman123";

            //usr1 = await UOW.UserRepository.CreateAsync(usr1);

            //Console.WriteLine("New catergory {0} created with id {1}", usr1.fullName, usr1.ID);

            Channel ch1 = new Channel();
            ch1.channelName = "hi";
            ch1 = await UOW.ChannelRepository.CreateAsync(ch1);
            Console.WriteLine("New channel {0} created with id {1}", ch1.channelName, ch1.Id);





        }

    }
    
}
