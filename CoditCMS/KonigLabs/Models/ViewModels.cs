using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KonigLabs.Models
{
    public class LandingPage
    {
        public List<ViewMember> Members { get; set; }
        //public List<Client> Clients { get; set; }
        //public List<Client> { get; set; }
    }

    public class ViewCategory
    {
        
        public string Token {get;set;}
        public string Name {get;set;}

        public ViewCategory(ProjectCategory category)
        {
            Token= String.Format("category-{}", category.Id);
            Name = category.Name;
        }
    }

    public class ViewProject
    {
        public ViewProject(Project project)
        {

        }

    }

    public class ViewMember
    {
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public ViewMember(CrewMember member)
        {
            Avatar = member.GetAvatarPath();
            Title = member.Title;
        }
    }
}