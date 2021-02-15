import { createFeatureSelector, createSelector } from '@ngrx/store';
import { StoreModules } from 'app/store';
import * as _ from 'lodash';
import * as fromContactEntries from '../reducers/contacts.reducer';
import { ContactsEntriesState } from '../reducers/contacts.reducer';

export const selectContactsEntriesState = createFeatureSelector<ContactsEntriesState>(StoreModules.ContactEntries);


export const selectContacts = () => createSelector(
    selectContactsEntriesState,
    fromContactEntries.selectAll
);

export const selectContactEntry = (id) => createSelector(
    selectContactsEntriesState,
    contactEntry => {
        const value = contactEntry.entities[id];
        return value;

    }

);
