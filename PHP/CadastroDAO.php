<?php

require_once 'UtilDAO.php';
require_once '../dao/Conexao.php';

class CadastroDAO extends Conexao{
    
    public function ValidarLogin($email, $senha)
    {
        if(trim($email) == '' || trim($senha) == ''){
            return 0;
        }
        
        $conexao = parent::retornaConexao();
        $comando_sql = 'select id_usuario from tb_usuario where email_usuario = ? and senha_usuario = ?';
        
        $sql = new PDOStatement();
        $sql = $conexao->prepare($comando_sql);
        $sql->bindValue(1, $email);
        $sql->bindValue(2, $senha);
        
        $sql->setFetchMode(PDO::FETCH_ASSOC);
        $sql->execute();
        $user = $sql->fetchAll();
        
        if(count($user) == 0){
            return -4;
        }
        else{
            $cod = $user[0]['id_usuario'];
            UtilDAO::CriarSessao($cod);
            header('location: gerenciar_categoria.php');
            exit;
        }
        
    }
    
    
    
    public function CadastrarUsuario($nome, $email, $senha, $repetirsenha) {
        if (trim($nome) == '' || trim($email) == '' || trim($senha) == '') {
            return 0;
        }else if($senha != $repetirsenha){
            return -3;
        }


        $conexao = parent::retornaConexao();

        $comando_sql = 'insert into tb_usuario (nome_usuario, email_usuario, senha_usuario) values(?,?,?)';

        $sql = new PDOStatement();

        $sql = $conexao->prepare($comando_sql);

        $sql->bindValue(1, $nome);
        $sql->bindValue(2, $email);
        $sql->bindValue(3, $senha);


        try {

            $sql->execute();

            return 1;
        } catch (Exception $exc) {
            echo $exc->getMessage();
            return -1;
        }
    }
    
    
    
    
    
    
}
