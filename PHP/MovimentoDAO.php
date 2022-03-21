<?php

require_once 'Conexao.php';
require_once 'UtilDAO.php';

class MovimentoDAO extends Conexao {

    public function LancarMovimento($tipo, $data, $valor, $categoria, $conta, $empresa, $obs) {

        if ($tipo == '' || trim($data) == '' || trim($valor) == '' || trim($categoria) == '' || trim($conta) == '' || trim($empresa) == '') {
            return 0;
        }

        $conexao = parent::retornaConexao();

        $comando_sql = 'insert into tb_movimento (tipo_movimento, data_movimento, valor_movimento, obs_movimento, id_empresa, id_conta, id_usuario, id_categoria) values(?,?,?,?,?,?,?,?)';

        $sql = new PDOStatement();

        $sql = $conexao->prepare($comando_sql);

        $sql->bindValue(1, $tipo);
        $sql->bindValue(2, $data);
        $sql->bindValue(3, $valor);
        $sql->bindValue(4, $obs);
        $sql->bindValue(5, $empresa);
        $sql->bindValue(6, $conta);
        $sql->bindValue(7, UtilDAO::CodigoLogado());
        $sql->bindValue(8, $categoria);

        $conexao->beginTransaction();

        try {
            //inserir na tb_mov
            $sql->execute();

            if ($tipo == 0) {
                $comando_sql = 'update tb_conta set saldo_conta=saldo_conta+? where id_conta = ?';
            } else if ($tipo == 1) {
                $comando_sql = 'update tb_conta set saldo_conta=saldo_conta-? where id_conta = ?';
            }

            $sql = $conexao->prepare($comando_sql);

            $sql->bindValue(1, $valor);
            $sql->bindValue(2, $conta);

            $sql->execute();
            $conexao->commit();

            return 1;
        } catch (Exception $ex) {
            $conexao->rollBack();
            return -1;
        }
    }

    public function PesquisarMovimento($tipo, $dtInicial, $dtFinal) {
        if (trim($dtInicial) == '' || trim($dtFinal) == '') {
            return 0;
        }

        $conexao = parent::retornaConexao();

        $comando_sql = 'select obs_movimento, 
                                id_movimento, 
                                tipo_movimento, 
                                valor_movimento, 
                                date_format(data_movimento, "%d/%m/%Y") as data_movimento, 
                                nome_categoria, 
                                nome_empresa, 
                                nome_conta,
                                tb_movimento.id_conta
                           from tb_movimento 
                     inner join tb_categoria 
                             on tb_movimento.id_categoria = tb_categoria.id_categoria 
                     inner join tb_empresa 
                             on tb_movimento.id_empresa = tb_empresa.id_empresa 
                     inner join tb_conta on tb_movimento.id_conta = tb_conta.id_conta 
                          where tb_movimento.id_usuario = ? and tb_movimento.data_movimento between ? and ?';

        if ($tipo != 2) {
            $comando_sql .= 'and tb_movimento.tipo_movimento = ?';
        }

        $sql = new PDOStatement();
        $sql = $conexao->prepare($comando_sql);

        $sql->bindValue(1, UtilDAO::CodigoLogado());
        $sql->bindValue(2, $dtInicial);
        $sql->bindValue(3, $dtFinal);

        if ($tipo != 2) {
            $sql->bindValue(4, $tipo);
        }

        $sql->setFetchMode(PDO::FETCH_ASSOC);

        $sql->execute();

        return $sql->fetchAll();
    }

    public function ExcluirMovimento2($cod) {
        $conexao = parent::retornaConexao();

        //select do movimento pra pegar4 o tipo
        $sqlS = new PDOStatement();
        $sqlS = $conexao->prepare('select tipo_movimento, id_conta,valor_movimento from tb_movimento where id_usuario = ?');

        $sqlS->bindValue(1, $cod);
        
        $sqlS->setFetchMode(PDO::FETCH_ASSOC);

        $sqlS->execute();

        $row = $sqlS->fetch();
        
        $tipo = $row['tipo_movimento'];
        $conta = $row['id_conta'];
        $valor = $row['valor_movimento'];
        
        $comando_sql = 'delete from tb_movimento where id_movimento = ? and id_usuario = ?';

        $sql = new PDOStatement();

        $sql = $conexao->prepare($comando_sql);

        $sql->bindValue(1, $cod);
        $sql->bindValue(2, UtilDAO::CodigoLogado());

        $conexao->beginTransaction();
        
        try {
            //inserir na tb_mov
            $sql->execute();
 
            if ($tipo == 0) {
                $comando_sql = 'update tb_conta set saldo_conta=saldo_conta+? where id_conta = ?';
            } else if ($tipo == 1) {
                $comando_sql = 'update tb_conta set saldo_conta=saldo_conta-? where id_conta = ?';
            }

            $sql = $conexao->prepare($comando_sql);

            $sql->bindValue(1, $valor);
            $sql->bindValue(2, $conta);

            $sql->execute();
            $conexao->commit();

            return 1;
        } catch (Exception $ex) {
            $conexao->rollBack();
            return -1;
        }
        
        
        
        
    }
    
    public function ExcluirMovimento($cod, $tipo, $valor, $codConta)
    {
        $conexao = parent::retornaConexao();
        
        $comando_sql = 'delete from tb_movimento where id_movimento = ?';
        
        $sql = new PDOStatement();
        $sql = $conexao->prepare($comando_sql);
        
        $sql->bindValue(1, $cod);
        
        $conexao->beginTransaction();
        
        try {
            $sql->execute();
            
            if($tipo==0){
                $comando_sql = 'update tb_conta set saldo_conta = saldo_conta - ? where id_conta = ?';
            } else if($tipo==1){
               $comando_sql = 'update tb_conta set saldo_conta = saldo_conta + ? where id_conta = ?';
            }
            
            $sql = $conexao->prepare($comando_sql);
            
            $sql->bindValue(1, $valor);
            $sql->bindValue(2, $codConta);
            
            $sql->execute();
            
            $conexao->commit();
            
            return 1;
            
        } catch (Exception $ex) {
            $conexao->rollBack();
        }
        
    }

}
