using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KonigLabs.Models
{
    public class LandingPage
    {
        public List<Member> Members { get; set; }
    }

    public class Member
    {
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public Member(CrewMember member)
        {
            Avatar = member.GetAvatarPath();
            Title = member.Title;
        }
    }
}