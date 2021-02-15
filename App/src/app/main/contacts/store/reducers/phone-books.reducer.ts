import { EntityState, createEntityAdapter } from '@ngrx/entity';
import { createReducer, on, Action } from '@ngrx/store';
import { BookDetailsModel } from '../../models/book-detail.model';
import { PhoneBookActions } from '../actions/phone-book.action-types';


export interface PhoneBookState extends EntityState<BookDetailsModel> {
}

export const phoneBookEntityAdapter = createEntityAdapter<BookDetailsModel>();

export const initialPhoneBookState = phoneBookEntityAdapter.getInitialState({
});

const phoneBookStateReducer = createReducer(
    initialPhoneBookState,
    on(PhoneBookActions.phoneBookLoaded, (state, action) => phoneBookEntityAdapter.addOne(action.phonebook, state)),
);

export const {
    selectAll,
    selectIds,
    selectEntities,
} = phoneBookEntityAdapter.getSelectors();

// tslint:disable-next-line:typedef
export function PhoneBookStateReducer(state: PhoneBookState | undefined, action: Action) {
    return phoneBookStateReducer(state, action);
}

