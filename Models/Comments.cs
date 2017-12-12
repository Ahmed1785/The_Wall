using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Wall.Models
{
    public class Comment
    {
        public int commentid { get; set; }
        public string comment { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public User User { get; set; }
        public Message Message { get; set; }
        // public List<User> postedcomments { get; set; }
        // public List<Message> messagecommented { get; set; }
        public int userid { get; set; }
        public int messageid { get; set; }

    }
}