using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TargetBot.Models
{
    public class EntityState
    {
        public EntityType EntityType { get; set; }

        public int Id { get; set; }

        public bool IsCommentRequired { get; set; }

        public bool IsFinal { get; set; }

        public bool IsInitial { get; set; }

        public bool IsPlanned { get; set; }

        public string Name { get; set; }

        public double NumericPriority { get; set; }

        public Process Process { get; set; }

        public Role Role { get; set; }

        public bool ShouldSerializeIsCommentRequires()
        {
            return false;
        }

        public bool ShouldSerializeIsFinal()
        {
            return false;
        }

        public bool ShouldSerializeIsInitial()
        {
            return false;
        }

        public bool ShouldSerializeIsPlanned()
        {
            return false;
        }

        public bool ShouldSerializeNumericPriority()
        {
            return false;
        }

        public bool ShouldSerializeProcess()
        {
            return false;
        }

        public bool ShouldSerializeRole()
        {
            return false;
        }
        public bool ShouldSerializeEntityType()
        {
            return false;
        }
        public bool ShouldSerializeName()
        {
            return false;
        }
    }

    public class EntityStates
    {
        public List<EntityState> Items { get; set; }

        public string Next { get; set; }
    }

    public class Process
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}