using System;
using System.Collections.Generic;

namespace FastTypesBenchmarks.Cloning
{
     public static class CloneInstance
    {
        public static CloneTarget DeepCopy(CloneTarget target)
        {
            var clonedRoot = new CloneTarget();

            if (target.Data != null)
            {
                clonedRoot.Data = new List<Data>(target.Data.Count);
                for (var i = 0; i < target.Data.Count; i++)
                {
                    var dataItem = target.Data[i];
                    var clonedData = dataItem.DeepCopy();
                    clonedRoot.Data.Add(clonedData);
                }
            }

            if (target.Included != null)
            {
                clonedRoot.Included = new List<Included>(target.Included.Count);
                foreach (var includedItem in target.Included)
                {
                    var clonedIncluded = includedItem.DeepCopy();
                    clonedRoot.Included.Add(clonedIncluded);
                }
            }

            return clonedRoot;
        }

        public static readonly CloneTarget Instance = new()
        {
            Data = new List<Data>()
            {
                new()
                {
                    Id = "1",
                    Type = "SampleType",
                    Attributes = new Attributes
                    {
                        Title = "Sample Title",
                        Body = "Sample Body",
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Name = "John Doe",
                        Age = 25,
                        Gender = "Male"
                    },
                    Relationships = new Relationships
                    {
                        Author = new Author
                        {
                            Data = new Data
                            {
                                Id = "2",
                                Type = "AuthorType",
                                Attributes = new Attributes
                                {
                                    Title = "Author Title",
                                    Body = "Author Body",
                                    Created = DateTime.Now,
                                    Updated = DateTime.Now,
                                    Name = "Jane Doe",
                                    Age = 30,
                                    Gender = "Female"
                                }
                            }
                        }
                    }
                }
            },

            Included = new List<Included>
            {
                new()
                {
                    Type = "IncludedType",
                    Id = "3",
                    Attributes = new Attributes
                    {
                        Title = "Included Title",
                        Body = "Included Body",
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Name = "Bob Smith",
                        Age = 28,
                        Gender = "Male"
                    }
                }
            }
        };
    }

    public class Attributes
    {
        public Attributes DeepCopy()
        {
            return new Attributes()
            {
                Title = Title,
                Body = Body,
                Created = Created,
                Updated = Updated,
                Name = Name,
                Age = Age,
                Gender = Gender
            };
        }

        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }

    public class Author
    {
        public Data Data { get; set; }

        public Author DeepCopy()
        {
            return new Author()
            {
                Data = Data.DeepCopy(),
            };
        }
    }

    public class Data
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public Attributes Attributes { get; set; }
        public Relationships Relationships { get; set; }

        public Data DeepCopy()
        {
            return new Data()
            {
                Id = Id,
                Type = Type,
                Attributes = Attributes?.DeepCopy(),
                Relationships = Relationships?.DeepCopy(),
            };
        }
    }

    public class Included
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public Attributes Attributes { get; set; }

        public Included DeepCopy()
        {
            return new Included()
            {
                Attributes = Attributes,
                Id = Id,
                Type = Type,
            };
        }
    }

    public class Relationships
    {
        public Relationships DeepCopy()
        {
            return new Relationships()
            {
                Author = Author.DeepCopy(),
            };
        }

        public Author Author { get; set; }
    }

    public class CloneTarget
    {
        public List<Data> Data { get; set; }
        public List<Included> Included { get; set; }
    }
}