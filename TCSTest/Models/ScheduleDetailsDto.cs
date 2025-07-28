using System;

namespace TCSTest.Models
{
    public class ScheduleDetailsDto: ScheduleBase
    {        
        public required string ChannelTitle { get; set; }        
        public required string ContentTitle { get; set; }
        
    }
}