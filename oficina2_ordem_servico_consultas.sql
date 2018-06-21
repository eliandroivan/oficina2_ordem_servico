DELETE FROM condicao_pagamento WHERE id = 4;

/* abertura OS */
select 
 ordem_servico.id, ordem_servico.numero, situacao.descricao as situacao, tipo_os.descricao as tipo_os, modelo.descricao as modelo, veiculo.placa as placa,  ordem_servico.quilometragem_entrada, cliente.nome as cliente_nome, consultor_usuario.nome as consultor_usuario,  ordem_servico.abertura,  ordem_servico.previsao_entrega,  ordem_servico.reclamacao_cliente
 from ordem_servico 
 inner join situacao on situacao.id=ordem_servico.id_situacao
 inner join tipo_os on tipo_os.id=ordem_servico.id_tipo_os
 inner join veiculo on veiculo.id=ordem_servico.id_veiculo
 inner join cliente on cliente.id=ordem_servico.id_cliente
 inner join consultor_usuario on consultor_usuario.id=ordem_servico.id_consultor_usuario
 inner join modelo on modelo.id=veiculo.id_modelo
 
 select max(numero) AS numero from ordem_servico;
 
 select
 ordem_servico.id, ordem_servico.numero, situacao.descricao as situacao, tipo_os.descricao as tipo_os, modelo.descricao as modelo, veiculo.placa as placa,  ordem_servico.quilometragem_entrada, cliente.nome as cliente_nome, consultor_usuario.nome as consultor_usuario,  ordem_servico.abertura,  ordem_servico.previsao_entrega,  ordem_servico.reclamacao_cliente
 from ordem_servico
 inner join situacao on situacao.id=ordem_servico.id_situacao
 inner join tipo_os on tipo_os.id=ordem_servico.id_tipo_os 
 inner join veiculo on veiculo.id=ordem_servico.id_veiculo
 inner join cliente on cliente.id=ordem_servico.id_cliente
 inner join consultor_usuario on consultor_usuario.id=ordem_servico.id_consultor_usuario
 inner join condicao_pagamento on condicao_pagamento.id=ordem_servico.id_condicao_pagamento
 inner join modelo on modelo.id=veiculo.id_modelo
 
  select
 ordem_servico.id, ordem_servico.numero, situacao.descricao as situacao, tipo_os.descricao as tipo_os, veiculo.placa as placa,  ordem_servico.quilometragem_entrada, cliente.nome as cliente_nome, consultor_usuario.nome as consultor_usuario,  ordem_servico.abertura,  ordem_servico.previsao_entrega,  ordem_servico.reclamacao_cliente
 from ordem_servico
 inner join situacao on situacao.id=ordem_servico.id_situacao
 inner join tipo_os on tipo_os.id=ordem_servico.id_tipo_os 
 inner join veiculo on veiculo.id=ordem_servico.id_veiculo
 inner join cliente on cliente.id=ordem_servico.id_cliente
 inner join consultor_usuario on consultor_usuario.id=ordem_servico.id_consultor_usuario
 inner join condicao_pagamento on condicao_pagamento.id=ordem_servico.id_condicao_pagamento


/* consulta itens na OS */
select 
os_produto.id, ordem_servico.numero as numero, tipo_produto.descricao as tipo_produto, produto.descricao as produto_descricao, os_produto.quantidade, produto.valor_unitario as valor_unitario , (os_produto.quantidade * produto.valor_unitario) as subtotal
from os_produto 
inner join ordem_servico on ordem_servico.id=os_produto.id_ordem_servico
inner join produto on produto.id=os_produto.id_produto
inner join tipo_produto on tipo_produto.id=produto.id_tipo_produto
WHERE numero=2

/* ValorTotalOS */
select 
SUM(os_produto.quantidade*produto.valor_unitario) as valor_total
from os_produto
inner join ordem_servico on ordem_servico.id=os_produto.id_ordem_servico
inner join produto on produto.id=os_produto.id_produto
where id_ordem_servico=1

/* Api OS */
SELECT 
ordem_servico.numero, situacao.descricao AS situacao, tipo_os.descricao AS tipo_os, veiculo.placa AS placa, ordem_servico.abertura, ordem_servico.valor_total
FROM ordem_servico
INNER JOIN veiculo ON veiculo.id=ordem_servico.id_veiculo
INNER JOIN tipo_os on tipo_os.id=ordem_servico.id_tipo_os 
INNER JOIN situacao ON situacao.id=ordem_servico.id_situacao 
WHERE id_situacao=2


/*abertura OS */
select
 ordem_servico.id, ordem_servico.numero, situacao.descricao as situacao, tipo_os.descricao as tipo_os, modelo.descricao as modelo, veiculo.placa as placa,  ordem_servico.quilometragem_entrada, cliente.nome as cliente_nome, consultor_usuario.nome as consultor_usuario,  ordem_servico.abertura,  ordem_servico.previsao_entrega,  ordem_servico.reclamacao_cliente
 from ordem_servico
 inner join situacao on situacao.id=ordem_servico.id_situacao
 inner join tipo_os on tipo_os.id=ordem_servico.id_tipo_os
 inner join veiculo on veiculo.id=ordem_servico.id_veiculo
 inner join cliente on cliente.id=ordem_servico.id_cliente
 inner join consultor_usuario on consultor_usuario.id=ordem_servico.id_consultor_usuario
 inner join modelo on modelo.id=veiculo.id_modelo 
 where ordem_servico.id_situacao=1 AND ordem_servico.id=1
 
/* fechamento OS */
SELECT 
ordem_servico.id, ordem_servico.numero, situacao.descricao AS situacao, cliente.nome, veiculo.placa, ordem_servico.entrega, condicao_pagamento.descricao AS pagamento, ordem_servico.valor_total 
FROM ordem_servico 
INNER JOIN situacao ON situacao.id=ordem_servico.id_situacao 
INNER JOIN veiculo ON veiculo.id=ordem_servico.id_veiculo 
INNER JOIN cliente ON cliente.id=ordem_servico.id_cliente 
LEFT JOIN condicao_pagamento ON condicao_pagamento.id=ordem_servico.id_condicao_pagamento 
WHERE 
(ordem_servico.id_situacao=2) OR (ordem_servico.id_situacao=3) 
AND ordem_servico.id=1 

/* fechamento OS: cadastrar */
UPDATE 
ordem_servico 
SET 
id_situacao=2, 
entrega="2018-05-31 10:00:00",
id_condicao_pagamento=1,
valor_total=11111
WHERE id=3