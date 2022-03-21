<?php

require_once 'UtilDAO.php';
require_once 'Conexao.php';

class EmpresaDAO extends Conexao {

    public function CadastrarEmpresa($nome, $telefone, $endereco) {
        if (trim($nome) == '' || trim($telefone) == '' || trim($endereco) == '') {
            return 0;
        }

        $conexao = parent::retornaConexao();

        $comando_sql = 'insert into tb_empresa (nome_empresa, telefone_empresa, endereco_empresa, id_usuario) values(?,?,?,?)';

        $sql = new PDOStatement();

        $sql = $conexao->prepare($comando_sql);

        $sql->bindValue(1, $nome);
        $sql->bindValue(2, $telefone);
        $sql->bindValue(3, $endereco);
        $sql->bindValue(4, UtilDAO::CodigoLogado());


        try {

            $sql->execute();

            return 1;
        } catch (Exception $exc) {
            //echo $exc->getMessage();
            return -1;
        }
    }

    public function AlterarEmpresa($nome, $telefone, $endereco, $cod) {
        if (trim($nome) == '' || trim($telefone) == '' || trim($endereco) == '') {
            return 0;
        }

        $conexao = parent::retornaConexao();

        $comando_sql = 'update tb_empresa set nome_empresa = ?, telefone_empresa = ?, endereco_empresa = ? where id_usuario = ? and id_empresa = ?';

        $sql = $conexao->prepare($comando_sql);

        $sql->bindValue(1, $nome);

        $sql->bindValue(2, $telefone);

        $sql->bindValue(3, $endereco);

        $sql->bindValue(4, UtilDAO::CodigoLogado());

        $sql->bindValue(5, $cod);





        try {

            $sql->execute();
            return 1;
        } catch (Exception $exc) {
            //echo $exc->getMessage();
            return -1;
        }
    }
    
    public function ConsultarEmpresa() {
        $conexao = parent::retornaConexao();

        $comando_sql = 'select id_empresa, nome_empresa, telefone_empresa, endereco_empresa from tb_empresa where id_usuario = ?';

        $sql = new PDOStatement();

        $sql = $conexao->prepare($comando_sql);

        $sql->bindValue(1, UtilDAO::CodigoLogado());

        //remove os index (PERFONMANCE)
        $sql->setFetchMode(PDO::FETCH_ASSOC);

        $sql->execute();

        return $sql->fetchAll();
    }
        public function ExcluirEmpresa($cod)
    {
        $conexao = parent::retornaConexao();
        
        $comando_sql = 'delete from tb_empresa where id_empresa = ? and id_usuario = ?';
        
        $sql = $conexao->prepare($comando_sql);
        
        $sql->bindValue(1, $cod);
        
        $sql->bindValue(2, UtilDAO::CodigoLogado());
        
        try {
            $sql->execute();
            return 1;
        } catch (Exception $exc) {
            return -2;
        }
            
        
    }

}
