# <ins>API:</ins> Alerts

&nbsp;
&nbsp;




## Item

Alerts are stored in an *InMemory* database during execution.\
An alert is a structured item with the following attributes:
- an <ins>id</ins>:\
  Integer, given by **API** at creation, immutable
- a  <ins>message</ins>:\
  Text, given by **user** at creation, mutable under condition
- an <ins>area</ins>:\
  Text, given by **user** at creation, mutable under condition
- a  <ins>status</ins>:\
  Integer, given by **API** at creation, mutable under condition
- a  <ins>createdAt</ins>:\
  Date, given by **API** at creation, immutable

&nbsp;

### Ranges

The **id** attribute begins with 0 and increase as long as we add alerts.\
It is unique among alerts: even after deletion, a previously used id can't\
be used again.

The **status** attribute can only have values 0, 1 or 2, corresponding\
respectivelly to alert states "DRAFT", "PUBLISHED" and "CANCELLED".\
At creation, it is set to DRAFT.

An alert in DRAFT status can only be modified into PUBLISHED or remain unchanged.\
An alert in PUBLISHED status can only be modified to CANCELLED or remain unchanged.\
An alert in CANCELLED status can't be modified.

The **Message** and **Area** attributes can only be modified if status is DRAFT.\
If the status is being modified at the same time (in the same request), we consider the **old** status as reference.\
The only case it can happen here is `DRAFT => PUBLISHED`, modifications are allowed then.

&nbsp;
&nbsp;




## Requests

There are 5 kinds of request you can operate with alerts:
- Get all alerts
- Get an individual alert by ID
- Create an alert
- Modify an individual alert by ID
- Delete an individual alert by ID

&nbsp;

### Get all alerts

To get all alerts, use a **GET** with URL path suffix `/api/Alert`.

&nbsp;

### Get an individual alert by ID

To get a specific alert by id, use a **GET** with URL path suffix `/api/Alert/#id#`.

&nbsp;

### Create an alert

To get a specific alert by id, use a **POST** with URL path suffix `/api/Alert` and the following headers:
- id
- message
- status
- area
- createdAt

They are all optional and the API will override the values that are not the be set by the user actually.

&nbsp;

### Modify an individual alert by ID

To modify a specific alert by id, use a **PUT** with URL path suffix `/api/Alert/#id#` and the following headers:
- id
- message
- status
- area
- createdAt

They are all optional but the API may refuse some modifications depending on the rules explained before.

&nbsp;

### Delete an individual alert by ID

To delete a specific alert by id, use a **DELETE** with URL path suffix `/api/Alert/#id#`.

&nbsp;

