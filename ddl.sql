create table chovevid_person (
    id              number not null primary key,
    first_name      varchar(50),
    last_name       varchar(50),
    phone_number    varchar(10)
);

create table chovevid_breeding_station (
    id          number not null primary key,
    name        varchar(200),
    reg_number  number
);

create table chovevid_dog (
    id                      number not null primary key,
    id_owner                number,
    id_breeding_station     number not null,
    name                    varchar(50) not null,
    state                   varchar(3),
    constraint fk_dog_person foreign key (id_owner) references chovevid_person(id),
    constraint fk_dog_breeding_station foreign key (id_breeding_station) references chovevid_breeding_station(id)
);

drop table chovevid_person;
drop table chovevid_dog;
drop table chovevid_breeding_station;