create database oficina2_ordem_servico;
use oficina2_ordem_servico;

create table situacao(
id int primary key auto_increment,
descricao varchar(100) not null
);

create table tipo_os(
id int primary key auto_increment,
descricao varchar(100) not null
);

create table marca(
id int primary key auto_increment,
descricao varchar(200) not null
);

create table modelo(
id int primary key auto_increment,
descricao varchar(200) not null,
id_marca int not null,
constraint modelo_fk_id_marca foreign key (id_marca) references marca(id)
);

create table estado(
id int primary key auto_increment,
nome varchar(50) not null,
sigla varchar(2) not null
);

create table cidade(
id int primary key auto_increment,
nome varchar(70) not null,
id_estado int not null,
constraint cidade_fk_id_estado foreign key (id_estado) references estado(id)
);


create table cliente(
id int primary key auto_increment,
nome varchar(200) not null,
cpf_cnpj varchar(14) not null,
email varchar(70) not null,
telefone1 varchar(14) not null,
telefone2 varchar(14),
logradouro varchar(200) not null,
logradouro_numero varchar(10) not null,
logradouro_complemento varchar(20),
bairro varchar(40) not null,
id_estado int not null,
id_cidade int not null,
cep varchar(9) not null,
constraint cliente_fk_id_cidade foreign key (id_cidade) references cidade(id),
constraint cliente_fk_id_estado foreign key (id_estado) references estado(id)
);

create table consultor_usuario(
id int primary key auto_increment,
nome varchar(200) not null,
cpf_cnpj varchar(14) not null,
email varchar(70) not null,
telefone1 varchar(14) not null,
telefone2 varchar(14),
logradouro varchar(200) not null,
logradouro_numero varchar(10) not null,
logradouro_complemento varchar(20),
bairro varchar(40) not null,
id_cidade int not null,
id_estado int not null,
cep varchar(9) not null,
login_usuario varchar(50) not null,
login_senha varchar(50) not null,
constraint consultor_usuario_fk_id_cidade foreign key (id_cidade) references cidade(id),
constraint consultor_usuario_fk_id_estado foreign key (id_estado) references estado(id)
);

create table veiculo(
id int primary key auto_increment,
id_cliente int not null,
id_marca int not null,
id_modelo int not null,
placa varchar(7) not null,
chassi varchar(17) not null,
ano_fabricacao int not null,
ano_modelo int not null,
cor varchar(30) not null,
constraint veiculo_fk_id_marca foreign key (id_marca) references marca(id),
constraint veiculo_fk_id_modelo foreign key (id_modelo) references modelo(id),
constraint veiculo_fk_id_cliente foreign key (id_cliente) references cliente(id)
);

create table condicao_pagamento(
id int primary key auto_increment,
descricao varchar(100) not null
);


create table ordem_servico(
id int primary key auto_increment,
numero varchar(10),
id_situacao int,
id_tipo_os int,
id_veiculo int,
quilometragem_entrada int,
id_cliente int,
id_consultor_usuario int,
abertura datetime,
previsao_entrega datetime,
entrega datetime,
reclamacao_cliente varchar(1000),
id_condicao_pagamento int,
valor_total float(10,2),
constraint ordem_servico_fk_id_situacao foreign key (id_situacao) references situacao(id),
constraint ordem_servico_fk_id_tipo_os foreign key (id_tipo_os) references tipo_os(id),
constraint ordem_servico_fk_id_cliente foreign key (id_cliente) references cliente(id),
constraint ordem_servico_fk_id_veiculo foreign key (id_veiculo) references veiculo(id),
constraint ordem_servico_fk_id_consultor_usuario foreign key (id_consultor_usuario) references consultor_usuario(id),
constraint ordem_servico_fk_id_condicao_pagamento foreign key (id_condicao_pagamento) references condicao_pagamento(id)
);

create table tipo_produto(
id int primary key auto_increment,
descricao varchar(100) not null
);

create table produto(
id int primary key auto_increment,
id_tipo_produto int not null,
referencia varchar(20) not null,
descricao varchar(200) not null,
unidade varchar(20) not null,
quantidade float(10,2) not null,
valor_unitario float(10,2) not null,
constraint produto_fk_id_tipo_produto foreign key (id_tipo_produto) references tipo_produto(id)
);



create table os_produto(
id int primary key auto_increment,
id_ordem_servico int not null,
id_produto int not null,
quantidade float(10,2) not null,
constraint os_produto_fk_id_ordem_servico foreign key (id_ordem_servico) references ordem_servico(id),
constraint os_produto_fk_id_produto foreign key (id_produto) references produto(id)
);
