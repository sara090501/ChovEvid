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
    created     date,
    constraint fk_breeding_station_person foreign key (id_owner) references chovevid_person(id)
);

create table chovevid_dog (
    id                      number not null primary key,
    id_owner                number not null,
    id_breeding_station     number not null,
    name                    varchar(50) not null,
    sex                     varchar(1) check (sex in ('F', 'M')),
    state                   varchar(50) check (state in ('REZERVOVANY', 'PREDANY', 'OSTAVA')),
    constraint fk_dog_person foreign key (id_owner) references chovevid_person(id),
    constraint fk_dog_breeding_station foreign key (id_breeding_station) references chovevid_breeding_station(id)
);


drop table chovevid_dog;
drop table chovevid_person;
drop table chovevid_breeding_station;

-- INSERTY DO TABUĽKY chovevid_person
INSERT INTO chovevid_person (id, first_name, last_name, phone_number, email) VALUES (1, 'John', 'Doe', '1234567890', 'john.doe@example.com');
INSERT INTO chovevid_person (id, first_name, last_name, phone_number, email) VALUES (2, 'Jane', 'Smith', '0987654321', 'jane.smith@example.com');
INSERT INTO chovevid_person (id, first_name, last_name, phone_number, email) VALUES (3, 'Michael', 'Johnson', '1122334455', 'michael.j@example.com');
INSERT INTO chovevid_person (id, first_name, last_name, phone_number, email) VALUES (4, 'Emily', 'Davis', '5566778899', 'emily.d@example.com');
INSERT INTO chovevid_person (id, first_name, last_name, phone_number, email) VALUES (5, 'Chris', 'Brown', '6677889900', 'chris.brown@example.com');

-- INSERTY DO TABUĽKY chovevid_breeding_station
INSERT INTO chovevid_breeding_station (id, id_owner, name, reg_number) VALUES (1, 1, 'Sunny Paws Breeding', 101);
INSERT INTO chovevid_breeding_station (id, id_owner, name, reg_number) VALUES (2, 2, 'Happy Tails Kennel', 102);
INSERT INTO chovevid_breeding_station (id, id_owner, name, reg_number) VALUES (3, 3, 'Puppy Paradise', 103);
INSERT INTO chovevid_breeding_station (id, id_owner, name, reg_number) VALUES (4, 4, 'Furry Friends Breeders', 104);
INSERT INTO chovevid_breeding_station (id, id_owner, name, reg_number) VALUES (5, 5, 'Loyal Companions Kennel', 105);

-- INSERTY DO TABUĽKY chovevid_dog
INSERT INTO chovevid_dog (id, id_owner, id_breeding_station, name, state) VALUES (1, 1, 1, 'Buddy', 'REZERVOVANY');
INSERT INTO chovevid_dog (id, id_owner, id_breeding_station, name, state) VALUES (2, 2, 2, 'Bella', 'PREDANY');
INSERT INTO chovevid_dog (id, id_owner, id_breeding_station, name, state) VALUES (3, 3, 3, 'Charlie', 'OSTAVA');
INSERT INTO chovevid_dog (id, id_owner, id_breeding_station, name, state) VALUES (4, 4, 4, 'Lucy', 'PREDANY');
INSERT INTO chovevid_dog (id, id_owner, id_breeding_station, name, state) VALUES (5, 5, 5, 'Max', 'REZERVOVANY');


commit;