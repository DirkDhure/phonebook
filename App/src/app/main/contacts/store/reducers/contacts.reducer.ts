import { EntityState, createEntityAdapter } from '@ngrx/entity';
import { createReducer, on, Action } from '@ngrx/store';
import { ContactEntryModel } from '../../models/contact-entry.model';
import { ContactsActions } from '../actions/contacts.action-types';


export interface ContactsEntriesState extends EntityState<ContactEntryModel> { }

export const contactsEntryEntityAdapter = createEntityAdapter<ContactEntryModel>();

export const initialContactsEntriesState = contactsEntryEntityAdapter.getInitialState({});

const contactsEntriesStateReducer = createReducer(
    initialContactsEntriesState,
    on(ContactsActions.contactsLoaded, (state, action) => contactsEntryEntityAdapter.addAll(action.contacts, state)),
    on(ContactsActions.contactLoaded, (state, action) => contactsEntryEntityAdapter.addOne(action.contact, state)),
    on(ContactsActions.updateContact, (state, action) => contactsEntryEntityAdapter.updateOne(action.update, state)),
);

export const {
    selectAll,
    selectIds,
    selectEntities,
} = contactsEntryEntityAdapter.getSelectors();

// tslint:disable-next-line:typedef
export function ContactsEntriesStateReducer(state: ContactsEntriesState | undefined, action: Action) {
    return contactsEntriesStateReducer(state, action);
}

