create database CLINICAUHBenavides;
go

use CLINICAUHBenavides;
go

create table Pacientes
(
Cedula varchar(20) not null primary key,
Nombre varchar(50)not null,
Apellidos varchar(50),
Telefono varchar(50),
FechaNacimiento Date,
Edad int
);
go

create table Medicos
(
ID int primary key not null,
Nombre varchar(50)not null,
Apellidos varchar(50),
Telefono varchar(50),
Especialidad varchar(50),
);
go

create table Detalles
(
NumeroConsulta int primary key not null,
Detalles text,
FechaAtencion Date default getdate(),
HoraAtencion time default cast(getdate() as time),
NumeroConsultorio int,
CedulaPaciente varchar(20),
IDMedico int,
foreign key (CedulaPaciente) references pacientes(Cedula),
foreign key (IDMedico) references Medicos(ID)
);
go