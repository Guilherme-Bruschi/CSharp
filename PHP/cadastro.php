<?php
require_once '../dao/CadastroDAO.php';

$objdao = new CadastroDAO();

if (isset($_POST['btnSalvar'])) {

    $nome = $_POST['nome'];

    $email = $_POST['email'];

    $senha = $_POST['senha'];

    $repetirsenha = $_POST['repetirsenha'];


    $ret = $objdao->CadastrarUsuario($nome, $email, $senha, $repetirsenha);
}
?>



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <?php
    include_once '_head.php';
    ?>
    <body>
        <div class="container">
            <div class="row text-center  ">
                <div class="col-md-12">

                    <br /><br />   
                    <?php
                    include_once '_msg.php';
                    ?>
                    <h2> Controle Financeiro : Cadastro</h2>

                    <h5>( Registre-se para ter acesso )</h5>
                    <br />
                </div>
            </div>
            <div class="row">

                <div class="col-md-4 col-md-offset-4 col-sm-6 col-sm-offset-3 col-xs-10 col-xs-offset-1">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <strong>  Preencha todos os campos! </strong>  
                        </div>
                        <div class="panel-body">

                            <br/>
                            <form action="cadastro.php" method="post">
                                <div class="form-group input-group">
                                    <span class="input-group-addon"><i class="fa fa-circle-o-notch"  ></i></span>
                                    <input type="text" class="form-control" placeholder="Seu nome" name="nome"/>
                                </div>
                                <div class="form-group input-group">
                                    <span class="input-group-addon">@</span>
                                    <input type="text" class="form-control" placeholder="Seu e-mail" name="email"/>
                                </div>
                                <div class="form-group input-group">
                                    <span class="input-group-addon"><i class="fa fa-lock"  ></i></span>
                                    <input type="password" class="form-control" placeholder="Sua senha" name="senha"/>
                                </div>
                                <div class="form-group input-group">
                                    <span class="input-group-addon"><i class="fa fa-lock"  ></i></span>
                                    <input type="password" class="form-control" placeholder="Reescreva sua senha" name="repetirsenha"/>
                                </div>

                                <button type="submit" class="btn btn-success" name="btnSalvar"> Cadastrar</button>

                                <hr />

                                Já cadastrado ?  <a href="login.php" >Clique aqui!</a>

                            </form>

                        </div>

                    </div>
                </div>


            </div>
        </div>

    </body>
</html>

