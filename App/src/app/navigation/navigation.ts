import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [
    {
        id       : 'applications',
        title    : 'Applications',
        translate: 'NAV.APPLICATIONS',
        type     : 'group',
        children : [
            {
                id       : 'contacts',
                title    : 'Contacts',
                translate: 'NAV.CONTACTS.TITLE',
                type     : 'item',
                icon     : 'account_box',
                url      : '/contacts',
                
            }
        ]
    }
];
