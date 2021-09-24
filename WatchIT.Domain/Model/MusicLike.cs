using System;
using System.Collections.Generic;
using System.Text;
using WatchIT.Domain.SeedWork;

namespace WatchIT.Domain.Model
{
    public class MusicLike : Reaction
    {

        public int musicId { get; set; }
        public Music Music { get; set; }
       
    }
}
