create table chovevid_person (
    id              number not null primary key,
    first_name      varchar(50) not null,
    last_name       varchar(50) not null,
    phone_number    varchar(10),
    email           varchar(50)
);

create table chovevid_breeding_station (
    id          number not null primary key,
    id_owner    number not null,
    name        varchar(200),
    reg_number  number,
    constraint fk_breeding_station_person foreign key (id_owner) references chovevid_person(id)
);

create table chovevid_dog (
    id                      number not null primary key,
    id_owner                number not null,
    id_breeding_station     number not null,
    name                    varchar(50) not null,
    state                   varchar(50) check (state in ('REZERVOVANY', 'PREDANY', 'OSTAVA')),
    constraint fk_dog_person foreign key (id_owner) references chovevid_person(id),
    constraint fk_dog_breeding_station foreign key (id_breeding_station) references chovevid_breeding_station(id)
);


drop table chovevid_dog;
drop table chovevid_person;
drop table chovevid_breeding_station;


-- Vloženie záznamov do chovevid_person
insert into chovevid_person (id, first_name, last_name, phone_number, email) 
values (1, 'John', 'Doe', '1234567890', 'john.doe@example.com');
insert into chovevid_person (id, first_name, last_name, phone_number, email) 
values (2, 'Jane', 'Smith', '0987654321', 'jane.smith@example.com');
insert into chovevid_person (id, first_name, last_name, phone_number, email) 
values (3, 'Alice', 'Brown', '5551234567', 'alice.brown@example.com');
insert into chovevid_person (id, first_name, last_name, phone_number, email) 
values (4, 'Bob', 'Johnson', '4449876543', 'bob.johnson@example.com');
insert into chovevid_person (id, first_name, last_name, phone_number, email) 
values (5, 'Charlie', 'Williams', '3335557777', 'charlie.williams@example.com');

-- Vloženie záznamov do chovevid_breeding_station
insert into chovevid_breeding_station (id, name, reg_number) 
values (1, 'Happy Paws Breeding', 1001);
insert into chovevid_breeding_station (id, name, reg_number) 
values (2, 'Golden Retriever Paradise', 1002);
insert into chovevid_breeding_station (id, name, reg_number) 
values (3, 'Labrador Kingdom', 1003);
insert into chovevid_breeding_station (id, name, reg_number) 
values (4, 'Poodle Palace', 1004);
insert into chovevid_breeding_station (id, name, reg_number) 
values (5, 'Bulldog Haven', 1005);

-- Vloženie záznamov do chovevid_dog
insert into chovevid_dog (id, id_owner, id_breeding_station, name, state) 
values (1, 1, 1, 'Buddy', 'REZERVOVANY');
insert into chovevid_dog (id, id_owner, id_breeding_station, name, state) 
values (2, 2, 2, 'Bella', 'PREDANY');
insert into chovevid_dog (id, id_owner, id_breeding_station, name, state) 
values (3, 3, 3, 'Charlie', 'OSTAVA');
insert into chovevid_dog (id, id_owner, id_breeding_station, name, state) 
values (4, 4, 4, 'Lucy', 'PREDANY');
insert into chovevid_dog (id, id_owner, id_breeding_station, name, state) 
values (5, 5, 5, 'Max', 'REZERVOVANY');

commit;