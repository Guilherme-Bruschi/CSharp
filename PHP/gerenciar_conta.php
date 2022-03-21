<?php
require_once '../dao/UtilDAO.php';
UtilDAO::VerificarLogado();
require_once '../dao/ContaDAO.php';
$objdao = new ContaDAO();
$cod = '';
$banco = '';
$numero = '';
$agencia = '';
$saldo = '';

if (isset($_POST['btnSalvar'])) {

    $banco = $_POST['banco'];

    $numero = $_POST['numero'];

    $agencia = $_POST['agencia'];

    $saldo = $_POST['saldo'];
    
    $cod = $_POST['cod'];

    if ($cod == '') {
        $ret = $objdao->CadastrarConta($banco, $numero, $agencia, $saldo);
    } else {
        $ret = $objdao->AlterarConta($banco, $agencia, $numero, $saldo, $cod);
    }
    $cod = '';
    $banco = '';
    $agencia = '';
    $saldo = '';
    $numero = '';
} else if (isset($_GET['cod']) && is_numeric($_GET['cod'])) {
    $cod = $_GET['cod'];
    $banco = $_GET['banco'];
    $agencia = $_GET['agencia'];
    $numero = $_GET['numero'];
    $saldo = $_GET['saldo'];
} else if (isset($_POST['btnExcluir'])) {
    $cod = $_POST['cod'];

    $ret = $objdao->ExcluirConta($cod);
    $cod = '';
    $nome = '';
    $endereco = '';
    $telefone = '';
}
$dados = $objdao->ConsultarConta();
?>
﻿<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <?php include_once '_head.php'; ?>
    <body>
        <div id="wrapper">
            <?php
            include_once '_topo.php';
            include_once '_menu.php';
            ?>

            <div id="page-wrapper" >
                <div id="page-inner">
                    <div class="row">
                        <div class="col-md-12">
                            <?php
                            include_once '_msg.php';
                            ?>
                            <h2>Gerenciar Contas</h2>   
                            <h5>Gerencie suas contas aqui</h5>

                        </div>
                    </div>
                    <!-- /. ROW  -->
                    <hr />
                    <form action="gerenciar_conta.php" method="post">
                        <input type="hidden" name="cod" value="<?= $cod ?>">
                            <div class="form-group">
                                <label>Nome do Banco</label>
                                <input class="form-control" placeholder="Digite aqui" name="banco" value="<?= $banco ?>"/>
                            </div>
                            <div class="form-group">
                                <label>Número da Conta</label>
                                <input class="form-control" placeholder="Digite aqui" name="numero" value="<?= $numero ?>"/>
                            </div>
                            <div class="form-group">
                                <label>Agência</label>
                                <input class="form-control" placeholder="Digite aqui" name="agencia" value="<?= $agencia ?>"/>
                            </div>
                            <div class="form-group">
                                <label>Saldo</label>
                                <input class="form-control" placeholder="Digite aqui" name="saldo" value="<?= $saldo ?>"/>
                            </div>
                            <button type="submit" class="btn btn-success"name="btnSalvar"> <?= $cod == '' ? 'Cadastrar' : 'Alterar' ?></button>
                            <button type="submit" class="btn btn-warning">Cancelar</button>
                            <?php if ($cod != '') { ?>      
                                <button type="submit" class="btn btn-danger"name="btnExcluir">Excluir</button>
                            <?php } ?>

                    </form>


                    <hr />

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Contas Cadastradas
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                    <thead>
                                        <tr>
                                            <th>Banco</th>
                                            <th>Número</th>
                                            <th>Agência</th>
                                            <th>Saldo</th>
                                            <th>Ação</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <?php for ($i = 0; $i < count($dados); $i++) { ?>

                                            <tr class="odd gradeX">
                                                <td> <?= $dados[$i]['nome_conta'] ?></td>
                                                <td> <?= $dados[$i]['numero_conta'] ?></td>
                                                <td> <?= $dados[$i]['agencia_conta'] ?></td>
                                                <td> <?= $dados[$i]['saldo_conta'] ?></td>
                                                <td> 

                                                    <a href="gerenciar_conta.php?cod=<?= $dados[$i]['id_conta'] ?>&banco=<?= $dados[$i]['nome_conta'] ?>&numero=<?= $dados[$i]['numero_conta'] ?>&agencia=<?= $dados[$i]['agencia_conta'] ?>&saldo=<?= $dados[$i]['saldo_conta'] ?>" class="btn btn-primary btn-sm"><i class="fa fa-edit"></i> Alterar </a> 

                                                </td>
                                            </tr>   
                                        <?php } ?>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /. PAGE INNER  -->
            </div>
            <!-- /. PAGE WRAPPER  -->
        </div>
    </body>
</html>

