# ToDoApp

Ilk olarak Couchbase kuruldu ve 'Admin' ve '478961' olarak sifreler belirlendi.

2 adet bucket olusturuldu. 'Users' ve 'TodoList'

'Users' yapisi su sekilde:
-----JSON
{
   
  "id": "1212212",
  "Name": "Serpil topcu",
  "Email": "topcuserpil@hotmail.com",
  "Password": "e10adc3949ba59abbe56e057f20f883e",
  "CreatedDate": "2021-08-07T21:32:53.8728108Z"
}
-----
'TodoList' yapisi ise su sekilde:
-----JSON
{
  "id": "1ad12395-3d5d-495f-97bb-30fefefba498",
  "title": "test",
  "content": "test",
  "createdByUser": {
    "id": "1212212",
    "name": "serpil topcu"
  },
  "updatedByUser": {
    "id": null,
    "name": null
  },
  "assignedToUser": {
    "id": "21221",
    "name": "semih topcu"
  },
  "createdDate": "2021-08-11T08:03:37.5176647Z",
  "updatedDate": null,
  "dueDate": "2021-08-19T21:00:00Z",
  "status": "Active"
}
-----
Add Couchbase Database Server configuration'i icin 'appsettings.json'`a asagidaki bilgiler eklendi:

----JSON
{
  "Couchbase": {
    "Servers": [
      "http://localhost:8091"
    ],
    "Username": "Admin",
    "Password": "478961",
    "UseSsl": false
  }
}
-----


