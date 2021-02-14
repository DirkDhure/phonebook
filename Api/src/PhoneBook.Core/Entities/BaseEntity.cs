using PhoneBook.Abstractions.Model;
using System;

namespace PhoneBook.Core.Entities
{

    public class BaseEntity : IEntity
    {
        private Guid _id;
        protected bool _isDeleted;
        
        public BaseEntity()
        {
        }
        protected BaseEntity(Guid id, bool isDeleted)
        {
            if (Equals(id, default(Guid)))
            {
                throw new ArgumentException("The ID cannot be the default value.", "id");
            }

            this._id = id;
            this._isDeleted = isDeleted;
        }
        public Guid Id
        {
            get { return this._id; }
            set { _id = value; }

        }

        public bool IsDeleted
        {
            get { return this._isDeleted; }
            set { _isDeleted = value; }
        }

        public bool IsNew { get; set; }

        public void SetAsDeleted()
        {
            this._isDeleted = true;
        }
    }
}
