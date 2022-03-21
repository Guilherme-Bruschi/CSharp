<?php
require_once '../dao/UtilDAO.php';
UtilDAO::VerificarLogado();
require_once '../dao/EmpresaDAO.php';

$objdao = new EmpresaDAO();
$cod = '';
$nome = '';
$telefone = '';
$endereco = '';


if (isset($_POST['btnSalvar'])) {

    $nome = $_POST['nome'];

    $endereco = $_POST['endereco'];

    $telefone = $_POST['telefone'];
    
    $cod = $_POST['cod'];

    if ($cod == '') {
        $ret = $objdao->CadastrarEmpresa($nome, $endereco, $telefone);
    } else {
        $ret = $objdao->AlterarEmpresa($nome, $telefone, $endereco, $cod);
    }
    $cod = '';
    $nome = '';
    $endereco = '';
    $telefone = '';
} else if (isset($_GET['cod']) && is_numeric($_GET['cod'])) {
    $cod = $_GET['cod'];
    $nome = $_GET['nome'];
    $telefone = $_GET['telefone'];
    $endereco = $_GET['endereco'];
} else if (isset($_POST['btnExcluir'])) {
    $cod = $_POST['cod'];

    $ret = $objdao->ExcluirEmpresa($cod);
    $cod = '';
    $nome = '';
    $endereco = '';
    $telefone = '';
}
$dados = $objdao->ConsultarEmpresa();
?>

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
                            <h2>Gerenciar Empresas</h2>   
                            <h5>Gerencie suas empresas aqui</h5>

                        </div>
                    </div>
                    <!-- /. ROW  -->
                    <hr />
                    <form action="gerenciar_empresa.php" method="post">
                        <input type="hidden" name="cod" value="<?= $cod ?>">
                            <div class="form-group">
                                <label>Nome da Empresa</label>
                                <input class="form-control" placeholder="Digite aqui" name="nome" value="<?= $nome ?>"/>
                            </div>
                            <div class="form-group">
                                <label>Telefone da Empresa</label>
                                <input class="form-control" placeholder="Digite aqui" name="telefone" value="<?= $telefone ?>"/>
                            </div>
                            <div class="form-group">
                                <label>Endereço da Empresa</label>
                                <input class="form-control" placeholder="Digite aqui" name="endereco" value="<?= $endereco ?>"/>
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
                            Empresas Cadastradas
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                    <thead>
                                        <tr>
                                            <th>Nome Empresa</th>
                                            <th>Telefone Empresa</th>
                                            <th>Endereço Empresa</th>
                                            <th>Ação</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <?php for ($i = 0; $i < count($dados); $i++) { ?>
                                            <tr class="odd gradeX">
                                                <td><?= $dados[$i]['nome_empresa'] ?></td>
                                                <td><?= $dados[$i]['telefone_empresa'] ?></td>
                                                <td><?= $dados[$i]['endereco_empresa'] ?></td>
                                                <td>

                                                    <a href="gerenciar_empresa.php?cod=<?= $dados[$i]['id_empresa'] ?>&nome=<?= $dados[$i]['nome_empresa'] ?>&telefone=<?= $dados[$i]['telefone_empresa'] ?>&endereco=<?= $dados[$i]['endereco_empresa'] ?>" class="btn btn-primary btn-sm"><i class="fa fa-edit"></i> Alterar </a> 

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

