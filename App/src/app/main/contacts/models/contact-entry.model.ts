import { ContactModel } from './contact.model';

export class ContactEntryModel {
    id: string;
    phoneBookId: string;
    firstName: string;
    lastName: string;
    companyName: string;
    contacts: ContactModel[];
}
