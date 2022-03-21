<?php

require_once 'UtilDAO.php';
require_once 'Conexao.php';

class CategoriaDAO extends Conexao {

    public function AlterarCategoria($nome, $cod) {
        if (trim($nome) == '') {
            return 0;
        }

        $conexao = parent::retornaConexao();

        $comando_sql = 'update tb_categoria set nome_categoria = ? where id_categoria = ? and id_usuario = ?';

        $sql = $conexao->prepare($comando_sql);

        $sql->bindValue(1, $nome);

        $sql->bindValue(2, $cod);

        $sql->bindValue(3, UtilDAO::CodigoLogado());



        try {

            $sql->execute();
            return 1;
        } catch (Exception $exc) {
            return -1;
        }
    }

    public function CadastrarCategoria($nome) {
        if (trim($nome) == '') {
            return 0;
        }


        $conexao = parent::retornaConexao();

        $comando_sql = 'insert into tb_categoria (nome_categoria, id_usuario) values(?,?)';

        $sql = new PDOStatement();

        $sql = $conexao->prepare($comando_sql);

        $sql->bindValue(1, $nome);
        $sql->bindValue(2, UtilDAO::CodigoLogado());

        try {

            $sql->execute();

            return 1;
        } catch (Exception $exc) {
            //echo $exc->getMessage();
            return -1;
        }
    }

    public function ConsultarCategoria() {
        $conexao = parent::retornaConexao();

        $comando_sql = 'select id_categoria, nome_categoria from tb_categoria where id_usuario = ?';

        $sql = new PDOStatement();

        $sql = $conexao->prepare($comando_sql);

        $sql->bindValue(1, UtilDAO::CodigoLogado());

        //remove os index (PERFONMANCE)
        $sql->setFetchMode(PDO::FETCH_ASSOC);

        $sql->execute();

        return $sql->fetchAll();
    }
    
    public function ExcluirCategoria($cod)
    {
        $conexao = parent::retornaConexao();
        
        $comando_sql = 'delete from tb_categoria where id_categoria = ? and id_usuario = ?';
        
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
