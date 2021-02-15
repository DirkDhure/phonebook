import { createFeatureSelector, createSelector } from '@ngrx/store';
import { StoreModules } from 'app/store';
import * as _ from 'lodash';
import { PhoneBookState } from '../reducers/phone-books.reducer';

export const selectPhoneBookState = createFeatureSelector<PhoneBookState>(StoreModules.ContactsPhoneBook);

export const selectCntactsPhonebook = () => createSelector(
    selectPhoneBookState,
    phonebooks => {
        if (phonebooks && phonebooks.entities) {

            for (const key in phonebooks.entities) {
                if (phonebooks.entities) {
                    const value = phonebooks.entities[key];
                    return value;
                }
            }
        } else {
            return undefined;
        }

    }
);

export const selectPhoneBooks = () => createSelector(
    selectPhoneBookState,
    contactsPhonebooks => {
        if (contactsPhonebooks.entities) {
            let filtered = [];
            filtered = _.filter(contactsPhonebooks.entities, (provider) => provider.id !== null);
            return filtered;
        } else {
            return [];
        }
    }
);

export const selectPhoneBook = () => createSelector(
    selectPhoneBookState,
    contactsPhonebooks => {
        if (contactsPhonebooks && contactsPhonebooks.ids) {
            return contactsPhonebooks.ids[0];
        } else {
            return undefined;
        }
    }
);
