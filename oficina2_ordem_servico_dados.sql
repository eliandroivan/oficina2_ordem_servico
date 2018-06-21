use oficina2_ordem_servico;

insert into situacao(descricao) values
("ABERTA"),
("FECHADA"),
("CANCELADA");

insert into tipo_os(descricao) values
("CLIENTE"),
("GARANTIA"),
("INTERNA");

insert into marca(descricao) values
("FIAT"),
("VOLKSWAGEN"),
("FORD"),
("VOLVO"),
("BMW"),
("RENAULT");

insert into modelo(descricao,id_marca) values
("PALIO",1),
("GOL",2),
("FOCUS",3),
("S60",4),
("528I",5),
("SANDERO",6);

insert into estado(nome,sigla) values
("SANTA CATARINA","SC"),
("PARANÁ","PR"),
("RIO GRANDE DO SUL","RS"),
("SÃO PAULO","SP"),
("RIO DE JANEIRO","RJ"),
("MINAS GERAIS","MG");

insert into cidade(nome,id_estado) values
("BLUMENAU",1),
("CURITIBA",2),
("PORTO ALEGRE",3),
("OSASCO",4),
("MACAÉ",5),
("OURO PRETO",6);

insert into cliente
(nome,cpf_cnpj,email,telefone1,telefone2,logradouro,logradouro_numero,logradouro_complemento,bairro,id_cidade,id_estado,cep) 
values
("ELIAS","04425013905","ELIAS@EMAIL.COM","(47)99625-1961","(47)99625-1961","RUA ALBERTO","100",null,"VILA NOVA",1,1,"89035-300"),
("DOUGLAS","04425013905","ELIAS@EMAIL.COM","(47)99625-1961","(47)99625-1961","RUA ALBERTO","100",null,"VILA NOVA",2,2,"89035-300"),
("ANDREIA","04425013905","ELIAS@EMAIL.COM","(47)99625-1961","(47)99625-1961","RUA ALBERTO","100",null,"VILA NOVA",3,3,"89035-300"),
("ROBERTA","04425013905","ELIAS@EMAIL.COM","(47)99625-1961","(47)99625-1961","RUA ALBERTO","100",null,"VILA NOVA",4,4,"89035-300"),
("ROBSON","04425013905","ELIAS@EMAIL.COM","(47)99625-1961","(47)99625-1961","RUA ALBERTO","100",null,"VILA NOVA",5,5,"89035-300"),
("MARLENE","04425013905","ELIAS@EMAIL.COM","(47)99625-1961","(47)99625-1961","RUA ALBERTO","100",null,"VILA NOVA",6,6,"89035-300");

insert into consultor_usuario
(nome,cpf_cnpj,email,telefone1,telefone2,logradouro,logradouro_numero,logradouro_complemento,bairro,id_cidade,id_estado,cep,login_usuario,login_senha) 
values
("FELIPE","04425013905","FELIPE@EMAIL.COM","(47)99625-1961","(47)99625-1961","RUA ALBERTO","100",null,"VILA NOVA",1,1,"89035-300","LOGIN","SENHA"),
("JORGE","04425013905","ELIAS@EMAIL.COM","(47)99625-1961","(47)99625-1961","RUA ALBERTO","100",null,"VILA NOVA",2,2,"89035-300","LOGIN","SENHA"),
("MATEUS","04425013905","ELIAS@EMAIL.COM","(47)99625-1961","(47)99625-1961","RUA ALBERTO","100",null,"VILA NOVA",3,3,"89035-300","LOGIN","SENHA"),
("FERNANDO","04425013905","ELIAS@EMAIL.COM","(47)99625-1961","(47)99625-1961","RUA ALBERTO","100",null,"VILA NOVA",4,4,"89035-300","LOGIN","SENHA"),
("CLAUDIO","04425013905","ELIAS@EMAIL.COM","(47)99625-1961","(47)99625-1961","RUA ALBERTO","100",null,"VILA NOVA",5,5,"89035-300","LOGIN","SENHA"),
("MORGANA","04425013905","ELIAS@EMAIL.COM","(47)99625-1961","(47)99625-1961","RUA ALBERTO","100",null,"VILA NOVA",6,6,"89035-300","LOGIN","SENHA");

insert into veiculo
(id_cliente,id_marca,id_modelo,chassi,placa,ano_fabricacao,ano_modelo,cor) 
values
(1,1,1,"9BWZZZ377VT004251","MFW3577",2012,2013,"AZUL"),
(2,2,2,"9BWZZZ377VT004252","MFW3576",2012,2013,"AZUL"),
(3,3,3,"9BWZZZ377VT004253","MFW3575",2012,2013,"AZUL"),
(4,4,4,"9BWZZZ377VT004254","MFW3574",2012,2013,"AZUL"),
(5,5,5,"9BWZZZ377VT004255","MFW3573",2012,2013,"AZUL"),
(6,6,6,"9BWZZZ377VT004256","MFW3572",2012,2013,"AZUL");

insert into condicao_pagamento(descricao) 
values
("DINHEIRO"),
("CARTÃO"),
("BOLETO");

insert into ordem_servico
(numero,id_situacao,id_tipo_os,id_veiculo,quilometragem_entrada,id_cliente,id_consultor_usuario,abertura,previsao_entrega,entrega,reclamacao_cliente,id_condicao_pagamento) 
values
("1",1,1,1,154987,1,1,"2018-05-30 09:00:00","2018-05-31 10:00:00","2018-05-31 10:00:00","RECLAMACAO",1),
("2",1,1,2,154986,2,2,"2018-05-30 09:00:00","2018-05-31 10:00:00","2018-05-31 10:00:00","RECLAMACAO",1),
("3",1,2,3,154985,3,3,"2018-05-30 09:00:00","2018-05-31 10:00:00","2018-05-31 10:00:00","RECLAMACAO",2),
("4",2,2,4,154984,4,4,"2018-05-30 09:00:00","2018-05-31 10:00:00","2018-05-31 10:00:00","RECLAMACAO",2),
("5",2,3,5,154983,5,5,"2018-05-30 09:00:00","2018-05-31 10:00:00","2018-05-31 10:00:00","RECLAMACAO",3),
("6",2,3,6,154982,6,6,"2018-05-30 09:00:00","2018-05-31 10:00:00","2018-05-31 10:00:00","RECLAMACAO",3);

insert into tipo_produto(descricao) values
("SERVIÇO"),
("PEÇA");

insert into produto
(id_tipo_produto,referencia,descricao,unidade,quantidade,valor_unitario) 
values
(1,"1","MÃO DE OBRA","HORA",10,200),
(2,"2","MOTOR GOL","UNIDADE",10,2000),
(2,"3","PNEU ARO 14 175/65","UNIDADE",10,200),
(2,"4","OLEO MOTOR SEMISINTETICO 15W40","LITRO",10,20),
(2,"5","ESPELHO RETROVISOR","UNIDADE",10,20),
(2,"6","PORTA DIANTEIRA DIREITA","UNIDADE",10,200);

insert into os_produto
(id_ordem_servico,id_produto,quantidade) 
values
(1,1,1),
(2,2,1),
(3,3,1),
(4,4,1),
(5,5,1),
(6,6,10);

