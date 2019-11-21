
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";


INSERT INTO empresa.pessoa_tipo(id, nome, data_criacao, ativo, operador) VALUES
('d0ec35c0-fcae-42b4-95d3-7534de9312be','Padrão', now(),TRUE, TRUE),
('4ebe419f-0611-4d5f-bcc6-38a806c3e593','Gerente',now(),TRUE, TRUE);

INSERT INTO empresa.pessoa (id, nome, sexo, data_criacao, ativo) VALUES
(uuid_generate_v4(),'Carlinhos', 'Masculino', now(),TRUE),
(uuid_generate_v4(),'João Vitor', 'Masculino',now(),TRUE),
(uuid_generate_v4(),'Luciana', 'Feminino',  now(),TRUE),
(uuid_generate_v4(),'Breno', 'Masculino',  now(),TRUE),
(uuid_generate_v4(),'André', 'Masculino',  now(),TRUE),
(uuid_generate_v4(),'Helton', 'Masculino', now(),TRUE),
(uuid_generate_v4(),'Neto', 'Masculino',  now(),TRUE),
(uuid_generate_v4(),'Catia', 'Feminino',  now(),TRUE),
(uuid_generate_v4(),'Fernanda', 'Feminino',  now(),TRUE),
(uuid_generate_v4(),'Henrique', 'Masculino', now(),TRUE),
(uuid_generate_v4(),'Emerson', 'Masculino',  now(),TRUE),
(uuid_generate_v4(),'Pedro', 'Masculino',  now(),TRUE),
(uuid_generate_v4(),'Rafael', 'Masculino', now(),TRUE);

INSERT INTO empresa.equipe (id, nome,   data_criacao, ativo) VALUES
('3b310af7-884f-4ea4-ad1b-929624f227aa','Equipe Paulo',   now(),TRUE),
('d32dc83c-a405-4a84-8ac3-66cb4dda9fce','Equipe José',   now(),TRUE);
	
INSERT INTO empresa.equipe_pessoa (id, equipe_id, pessoa_id,data_criacao, ativo) VALUES
(uuid_generate_v4(), '3b310af7-884f-4ea4-ad1b-929624f227aa', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Carlinhos'),now(),TRUE),
(uuid_generate_v4(), '3b310af7-884f-4ea4-ad1b-929624f227aa', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'João Vitor'),now(),TRUE),
(uuid_generate_v4(), '3b310af7-884f-4ea4-ad1b-929624f227aa', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Luciana'),now(),TRUE),
(uuid_generate_v4(), '3b310af7-884f-4ea4-ad1b-929624f227aa', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Breno'),now(),TRUE),
(uuid_generate_v4(), '3b310af7-884f-4ea4-ad1b-929624f227aa', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'André'),now(),TRUE),
(uuid_generate_v4(), '3b310af7-884f-4ea4-ad1b-929624f227aa', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Helton'),now(),TRUE);

INSERT INTO empresa.equipe_pessoa (id, equipe_id, pessoa_id,data_criacao, ativo) VALUES
(uuid_generate_v4(), 'd32dc83c-a405-4a84-8ac3-66cb4dda9fce', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Neto'),now(),TRUE),
(uuid_generate_v4(), 'd32dc83c-a405-4a84-8ac3-66cb4dda9fce', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Catia'),now(),TRUE),
(uuid_generate_v4(), 'd32dc83c-a405-4a84-8ac3-66cb4dda9fce', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Fernanda'),now(),TRUE),
(uuid_generate_v4(), 'd32dc83c-a405-4a84-8ac3-66cb4dda9fce', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Henrique'),now(),TRUE),
(uuid_generate_v4(), 'd32dc83c-a405-4a84-8ac3-66cb4dda9fce', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Rafael'),now(),TRUE),
(uuid_generate_v4(), 'd32dc83c-a405-4a84-8ac3-66cb4dda9fce', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Emerson'),now(),TRUE),
(uuid_generate_v4(), 'd32dc83c-a405-4a84-8ac3-66cb4dda9fce', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Pedro'),now(),TRUE);


INSERT INTO empresa.pessoa_categoria_pessoa
(id, data_criacao, ativo, pessoa_tipo_id, pessoa_id)    VALUES 
(uuid_generate_v4(), now(), TRUE, '4ebe419f-0611-4d5f-bcc6-38a806c3e593', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Pedro'),
(uuid_generate_v4(), now(), TRUE, 'd0ec35c0-fcae-42b4-95d3-7534de9312be', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Pedro'),
(uuid_generate_v4(), now(), TRUE, '4ebe419f-0611-4d5f-bcc6-38a806c3e593', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Emerson'),
(uuid_generate_v4(), now(), TRUE, '4ebe419f-0611-4d5f-bcc6-38a806c3e593', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Catia'), 
(uuid_generate_v4(), now(), TRUE, 'd0ec35c0-fcae-42b4-95d3-7534de9312be', (SELECT id FROM rehagro.pessoa pessoa WHERE pessoa.nome = 'Rafael');


