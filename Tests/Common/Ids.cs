﻿using MongoDB.Bson;

namespace Tests.Common
{
    public class Ids
    {
        public int UserAId { get; set; } = 1;
        public int UserBId { get; set; } = 2;
        public int UserCId { get; set; } = 3;
        public int UserDId { get; set; } = 4;

        public string ServerIdForDelete { get; set; }
        public string ServerIdForUpdate { get; set; }

        public string Channel1 { get; set; }
        public string Channel2 { get; set; }

        public string PrivateChat3 { get; set; }
        public string PrivateChat4 { get; set; }
        public string PrivateChat5 { get; set; }
        public string PrivateChat6 { get; set; }
        public string PrivateChat7 { get; set; } 
        
        public string Message1 { get; set; } 
        public string Message2 { get; set; } 
    }
}