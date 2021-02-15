using PhoneBook.Abstractions.Events;
using PhoneBook.Abstractions.Model;
using PhoneBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook.Core.Aggregates
{
    public class PhoneBookAggregate : BaseAggregate<PhoneBookEntity>
    {
        ValidationResult validationResult = new ValidationResult() { 
        
        };

        public PhoneBookAggregate(PhoneBookEntity entity) : base(entity)
        {

        }

        public ValidationResult CreatePhoneBook(PhoneBookDetail phoneBookDetail)
        {
            var result = ValidatePhoneBook(phoneBookDetail);
            if (result.IsValid)
            {
                SetDetails(phoneBookDetail);
                phoneBookDetail.Id = Entity.Id;
                AddEvent(new PhoneBookCreated(phoneBookDetail));
            }
            return result;
        }

        public ValidationResult UpdatePhoneBook(PhoneBookDetail phoneBookDetail)
        {
            var result = ValidatePhoneBookName(phoneBookDetail);
            if (result.IsValid)
            {
                SetName(phoneBookDetail);
                AddEvent(new PhoneBookNameUpdated(phoneBookDetail));
            }
            return result;

        }

        public ValidationResult CreatePhoneBookEntry(PhoneBookEntry entry)
        {
            var result = ValidatePhoneBookEntry(entry);
            if (result.IsValid)
            {
                entry.PhoneBookId = Entity.Id;
                SaveEntry(entry);

                AddEvent(new PhoneBookEntryCreated(entry));
            }
            return result;
        }


        public ValidationResult UpdatePhoneBookEntry(PhoneBookEntry entry)
        {
            var result = ValidatePhoneBookEntry(entry);
            if (result.IsValid)
            {
                entry.PhoneBookId = Entity.Id;
                SaveEntry(entry);

                AddEvent(new PhoneBookEntryUpdated(entry));
            }
            return result;
        }
        public void DeletePhoneBook()
        {
            Entity.IsDeleted = true;
            AddEvent(new PhoneBookDeleted(new DeleteEntity
            {
                EntityId = Entity.Id
            }));
        }

        public void DeletePhoneEntryBook(Guid entryId)
        {
                        var entry = Entity.Entries.FirstOrDefault(e => e.Id == entryId);
            if (entry != null)
            {
                entry.IsDeleted = true;
                AddEvent(new PhoneBookDeleted(new DeleteEntity
                {
                    EntityId = entryId,
                    RootEntityId = Entity.Id
                }));

            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneBookDetail"></param>
        /// <returns></returns>
        private ValidationResult ValidatePhoneBook(PhoneBookDetail phoneBookDetail)
        {
            // to do phone book validatiosn here
            return validationResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneBookDetail"></param>
        /// <returns></returns>
        private ValidationResult ValidatePhoneBookName(PhoneBookDetail phoneBookDetail)
        {
            //to do name validations here
            return validationResult;
        }

        private ValidationResult ValidatePhoneBookEntry(PhoneBookEntry entry)
        {

            //to do validations here
            return validationResult;
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneBookDetail"></param>
        private void SetDetails(PhoneBookDetail phoneBookDetail)
        {
            phoneBookDetail.OwnerEmail = phoneBookDetail.OwnerEmail.ToLower();
            phoneBookDetail.Id = Entity.Id;
            Entity.BookDetail = phoneBookDetail;
            
        }

        private void SaveEntry(PhoneBookEntry entry)
        {
            entry.PhoneBookId = Entity.Id;
            if (Entity.Entries == null)
            {
                Entity.Entries = new List<PhoneBookEntry>();
            }
            var existingEntry = Entity.Entries.FirstOrDefault(e => e.Id == entity.Id);
            if (existingEntry != null)
            {
                Entity.Entries.Remove(existingEntry);
            }
            Entity.Entries.Add(entry);
        }
        private void SetName(PhoneBookDetail phoneBookDetail)
        {
            phoneBookDetail.Id = Entity.Id;
            Entity.BookDetail.Name = phoneBookDetail.Name;
        }

    }
}
