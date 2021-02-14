using System;

namespace PhoneBook.Abstractions.Model
{
    public class BaseEntity : IEntity
    {
        private Guid _id;
        protected bool _isDeleted;
        protected bool _isNew;
        public BaseEntity() { }

        protected BaseEntity(Guid id, bool isDeleted)
        {
            this._id = id;
            this._isDeleted = isDeleted;
        }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
        public bool IsNew
        {
            get { return _isNew; }
            set { _isNew = value; }
        }

        public void Delete()
        {
            this._isDeleted = true;
        }

        public void MakeNew()
        {
            this._isNew = true;
        }
    }
}
