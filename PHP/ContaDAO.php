<?php

require_once 'UtilDAO.php';
require_once 'Conexao.php';

class ContaDAO extends Conexao {

    public function CadastrarConta($banco, $numero, $agencia, $saldo) {
        if (trim($banco) == '' || trim($numero) == '' || trim($agencia) == '' || trim($saldo) == '') {
            return 0;
        }

        $conexao = parent::retornaConexao();

        $comando_sql = 'insert into tb_conta (nome_conta, agencia_conta, numero_conta, saldo_conta, id_usuario) values(?,?,?,?,?)';

        $sql = new PDOStatement();

        $sql = $conexao->prepare($comando_sql);

        $sql->bindValue(1, $banco);
        $sql->bindValue(2, $numero);
        $sql->bindValue(3, $agencia);
        $sql->bindValue(4, $saldo);
        $sql->bindValue(5, UtilDAO::CodigoLogado());


        try {

            $sql->execute();

            return 1;
        } catch (Exception $exc) {
            //echo $exc->getMessage();
            return -1;
        }
    }

    public function ConsultarConta() {
        $conexao = parent::retornaConexao();

        $comando_sql = 'select id_conta, nome_conta, agencia_conta, numero_conta, saldo_conta from tb_conta where id_usuario = ?';

        $sql = new PDOStatement();

        $sql = $conexao->prepare($comando_sql);

        $sql->bindValue(1, UtilDAO::CodigoLogado());

        //remove os index (PERFONMANCE)
        $sql->setFetchMode(PDO::FETCH_ASSOC);

        $sql->execute();

        return $sql->fetchAll();
    }

    public function AlterarConta($banco, $agencia, $numero, $saldo, $cod) {
        if (trim($banco) == '' || trim($numero) == '' || trim($agencia) == '' || trim($saldo) == '') {
            return 0;
        }

        $conexao = parent::retornaConexao();

        $comando_sql = 'update tb_conta set nome_conta = ?, agencia_conta = ?, numero_conta = ?, saldo_conta = ? where id_usuario = ? and id_conta = ?';

        $sql = $conexao->prepare($comando_sql);

        $sql->bindValue(1, $banco);

        $sql->bindValue(2, $agencia);

        $sql->bindValue(3, $numero);

        $sql->bindValue(4, $saldo);

        $sql->bindValue(5, UtilDAO::CodigoLogado());

        $sql->bindValue(6, $cod);





        try {

            $sql->execute();
            return 1;
        } catch (Exception $exc) {
            return -1;
        }
    }

    public function ExcluirConta($cod) {
        $conexao = parent::retornaConexao();

        $comando_sql = 'delete from tb_conta where id_conta = ? and id_usuario = ?';

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
