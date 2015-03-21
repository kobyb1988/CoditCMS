using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class Location : IVisibleEntity, ISortableEntity, IHaveAliasEntity, IMetadataEntity, INestedEntity<Location>
    {
        
        public global::System.Int32 Id { get; set; }
        
        public Nullable<global::System.Int32> ParentId { get; set; }
        
        public virtual Location Parent {get;set;}

        public ICollection<Location> Locations { get; set; } 

        //public Nullable<global::System.Int32> GalleryId { get; set; }
        
        public global::System.String Name { get; set; }
        
        public global::System.String Alias { get; set; }
        
        public global::System.String Teaser { get; set; }
        
        public global::System.String Content { get; set; }
        
        public global::System.Boolean Visibility { get; set; }
        
        public global::System.Int32 Sort { get; set; }
        
        public global::System.String MetaTitle { get; set; }
        
        public global::System.String MetaKeywords { get; set; }
        
        public global::System.String MetaDescription { get; set; }
                
        public global::System.Boolean ShowInMenu { get; set; }
        


        public int Level { get; set; }
        public bool HasChilds { get; set; }
        
        public EntityCollection<Location> Children
        {
            get { 
                var entityCollection = new EntityCollection<Location>();
                foreach (var location in Locations)
                {
                    entityCollection.Add(location);
                }
                return entityCollection; 
            }
            set { }
        }

        //public Location Parent
        //{
        //    get { return Location1; }
        //    set { Location1 = value; }
        //}

        //public override string ToString()
        //{
        //    return Name;
        //}

        //public static Location Empty
        //{
        //    get { return new Location(); }
        //}

        //public Location Root
        //{
        //    get
        //    {
        //        return Parent == null ? this : Parent.Root;
        //    }
        //}

        //public bool InPath(string alias)
        //{
        //    if (Alias == alias)
        //        return true;
        //    if (ParentId == null)
        //        return false;
        //    return Parent.InPath(alias);
        //}
    }
}
