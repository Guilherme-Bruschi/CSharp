<?php
require_once '../dao/UtilDAO.php';
UtilDAO::VerificarLogado();
require_once '../dao/CategoriaDAO.php';
$objdao = new CategoriaDAO();
$cod = '';
$nome = '';

//Verifica se existe no post o name do botão
if (isset($_POST['btnSalvar'])) {
    $nome = $_POST['nome'];
    $cod = $_POST['cod'];

    if ($cod == '') {
        $ret = $objdao->CadastrarCategoria($nome);
    } else {
        $ret = $objdao->AlterarCategoria($nome, $cod);
    }
    $cod = '';
    $nome = '';
} else if (isset($_GET['cod']) && is_numeric($_GET['cod'])) {
    $cod = $_GET['cod'];
    $nome = $_GET['nome'];
} else if (isset($_POST['btnExcluir'])) {
    $cod = $_POST['cod'];

    $ret = $objdao->ExcluirCategoria($cod);
    $cod = '';
    $nome = '';
}



$dados = $objdao->ConsultarCategoria();
?>﻿
<!DOCTYPE html>
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
                            <h2>Gerenciar Categorias</h2>   
                            <h5>Gerencie suas categorias aqui</h5>

                        </div>
                    </div>
                    <!-- /. ROW  -->
                    <hr />
                    <form action="gerenciar_categoria.php" method="post">
                        <input type="hidden" name="cod" value="<?= $cod ?>">
                            <div class="form-group">
                                <label>Nome da Categoria</label>
                                <input class="form-control" placeholder="Digite aqui" name="nome" value="<?= $nome ?>"/>
                            </div>



                            <button type="submit" class="btn btn-success" name="btnSalvar"> <?= $cod == '' ? 'Cadastrar' : 'Alterar' ?></button>
                            <button type="submit" class="btn btn-warning">Cancelar</button>
                            <?php if ($cod != '') { ?>      
                                <button type="submit" class="btn btn-danger"name="btnExcluir">Excluir</button>
                            <?php } ?>
                    </form>

                    <hr />

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Categorias Cadastradas
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                    <thead>
                                        <tr>
                                            <th>Nome Categoria</th>
                                            <th>Ação</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <?php for ($i = 0; $i < count($dados); $i++) { ?>
                                            <tr class="odd gradeX">
                                                <td><?= $dados[$i]['nome_categoria'] ?></td>
                                                <td> 

                                                    <a href="gerenciar_categoria.php?cod=<?= $dados[$i]['id_categoria'] ?>&nome=<?= $dados[$i]['nome_categoria'] ?>" class="btn btn-primary btn-sm"><i class="fa fa-edit"></i> Alterar </a> 

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

