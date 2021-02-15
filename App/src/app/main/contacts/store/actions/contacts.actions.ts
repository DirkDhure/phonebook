import { Update } from '@ngrx/entity';
import { createAction, props } from '@ngrx/store';
import { ContactEntryModel } from '../../models/contact-entry.model';

export const contactsLoaded = createAction('[Phonebook service] Phonebook contacts loaded', props<{ contacts: ContactEntryModel[] }>());
export const contactLoaded = createAction('[Phonebook service] Phonebook contact loaded', props<{ contact: ContactEntryModel }>());
export const addContact = createAction('[Phonebook service] Add phonebook contact', props<{ contact: ContactEntryModel }>());
export const updateContact = createAction('[Phonebook service] Update phonebook contact', props<{ update: Update<ContactEntryModel> }>());
