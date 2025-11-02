CREATE TABLE public.roles (
	guid uuid NOT NULL,
	"name" varchar(100) NOT NULL,
	created_at timestamp NOT NULL,
	updated_at timestamp NOT NULL,
	CONSTRAINT roles_pk PRIMARY KEY (guid)
);

CREATE TABLE public.companies (
	guid uuid NOT NULL,
	"name" varchar(100) NOT NULL,
	description text NULL,
	status int4 NOT NULL,
	created_at timestamp NOT NULL,
	updated_at timestamp NOT NULL,
	cnpj varchar(14) NOT NULL,
	CONSTRAINT companies_pk PRIMARY KEY (guid)
);

CREATE TABLE public.employees (
	guid uuid NOT NULL,
	"name" varchar(100) NOT NULL,
	cpf varchar(11) NOT NULL,
	role_id uuid NOT NULL,
	company_id uuid NOT NULL,
	status int4 NOT NULL,
	created_at timestamp NOT NULL,
	updated_at timestamp NOT NULL,
	CONSTRAINT employees_key_cpf UNIQUE (cpf),
	CONSTRAINT employees_pk PRIMARY KEY (guid)
);

