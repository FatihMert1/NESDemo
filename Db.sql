--create database Nilvera;
use Nilvera;

create table InvoiceInfo(
	uuid varchar(36) primary key not null,
	send_invoice_type varchar(20) ,
	invoice_seri_or_number varchar(50),
	issue_date datetime
);

create table AddressInfo(
	id varchar(36) primary key not null,
	address varchar(50) ,
	district varchar(50) ,
	city varchar(50) ,
	country varchar(50) 
);

create table PartyInfo(
	register_number varchar(50) primary key not null,
	name varchar(50) not null,
	address varchar(250) not null,
	district varchar(50) not null,
	city varchar(50) not null,
	country varchar(50) not null
);


create table ExportCustomerInfo(
	party_name varchar(50) not null,
	person_name varchar(50) not null,
	address_info_id varchar(36),
	
	constraint fk_exportCustomerInfo_address foreign key (address_info_id)
	references AddressInfo(id)
	on delete no action
);

create table InvoiceLine(
	id varchar(36) not null primary key,
	[index] varchar(50) ,
	seller_code varchar ,
	name varchar(50)
);

create table Note(
	id varchar(36) not null primary key,
	note varchar(250) not null
);

create table NESInvoice(
	id varchar(36) primary key not null,
	invoice_info_id varchar(36) ,
	company_info_id varchar(50) ,
	customer_info_id varchar(50) ,
	ise_archive_invoice bit 

	constraint fk_nesInvoice_invoiceInfo foreign key (invoice_info_id) references InvoiceInfo(uuid),
	constraint fk_nesInvoice_companyInfo foreign key (company_info_id) references PartyInfo(register_number),
	constraint fk_nesInvoice_customerInfo foreign key (customer_info_id) references PartyInfo(register_number)

);

create table NESInvoice_InvoiceLine(
	id varchar(36) primary key not null,
	nesinvoice_id varchar(36) not null,
	invoice_line varchar(36) not null
	constraint fk_nesinvoiceInvoiceline_nesinvoice foreign key (nesinvoice_id) references NESInvoice(id),
	constraint fk_nesinvoiceInvoiceLine_invoiceline foreign key (invoice_line) references InvoiceLine(id)
);

create table NESInvoice_Note(
	id varchar(36) primary key not null,
	nesinvoice_id varchar(36) not null,
	note_id varchar(36) not null
	constraint fk_nesinvoiceNote_nesinvoice foreign key (nesinvoice_id) references NESInvoice(id),
	constraint fk_nesinvoiceNote_note foreign key (nesinvoice_id) references Note(id),
);

--use master;
--drop database Nilvera;