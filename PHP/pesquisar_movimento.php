<?php

require_once '../dao/UtilDAO.php';
UtilDAO::VerificarLogado();
require_once '../dao/MovimentoDAO.php';
$objdao = new MovimentoDAO();
$tipo = '';
$dtInicial = '';
$dtFinal = '';



if (isset($_POST['btnPesquisar'])) {

    $tipo = $_POST['tipo'];
    $dtInicial = $_POST['dtInicial'];
    $dtFinal = $_POST['dtFinal'];

    $movs = $objdao->PesquisarMovimento($tipo, $dtInicial, $dtFinal);
    
    if($movs == 0)
    {
        $ret = 0;
    }
        
} else if (isset($_GET['cod']) && isset($_GET['tipo']) && isset($_GET['con']) && isset($_GET['valor'])) {
    $cod = $_GET['cod'];
    $tipo = $_GET['tipo'];
    $valor = $_GET['valor'];
    $con = $_GET['con'];
    
    $ret = $objdao->ExcluirMovimento($cod, $tipo, $valor, $con);
}
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
                            <h2>Meus Movimentos</h2>   
                            <h5>Pesquise seus Movimentos aqui!</h5>

                        </div>
                    </div>
                    <!-- /. ROW  -->
                    <hr />
                    <form action="pesquisar_movimento.php" method="post">
                        <input type="hidden" name="cod" value="<?= $cod ?>">


                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Selecionar Movimento</label>
                                    <select class="form-control" name="tipo">
                                        <option value="2" <?= $tipo == '2' ? 'selected' : '' ?>>Todos</option>
                                        <option value="0" <?= $tipo == '0' ? 'selected' : '' ?>>Entrada</option>
                                        <option value="1" <?= $tipo == '1' ? 'selected' : '' ?>>Saída</option>
                                    </select>
                                </div>
                            </div>

                            <div class="col-md-6">

                                <div class="form-group">
                                    <label> Data Inicial</label>
                                    <input type="Date" class="form-control" name="dtInicial" value="<?= $dtInicial ?>">                                                                  
                                </div>

                            </div>

                            <div class="col-md-6">

                                <div class="form-group">
                                    <label> Data Final</label>
                                    <input type="Date" class="form-control" name="dtFinal" value="<?= $dtFinal ?>">                                                                      
                                </div>

                            </div>

                            <center><button type="submit" class="btn btn-primary" name="btnPesquisar">Pesquisar</button></center>
                    </form>
                    <hr />
                   <?php 
                   if(isset($movs) && $movs != 0) {
                   
                   ?>
                        <div class="panel panel-default">
                            <div class="panel-heading">

                                Movimentos Encontrados
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                        <thead>
                                            <tr>
                                                <th>Tipo</th>
                                                <th>Valor</th>
                                                <th>Data</th>
                                                <th>Categoria</th>
                                                <th>Empresa</th>
                                                <th>Conta</th>
                                                <th>Obs</th>
                                                <th>Ação</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <?php
                                            for ($i = 0; $i < count($movs); $i++) {
                                                
                                                
                                                $parametros = '?cod=' . $movs[$i]['id_movimento'] . '&tipo=' . $movs[$i]['tipo_movimento'] . '&con=' . $movs[$i]['id_conta'] . '&valor=' . $movs[$i]['valor_movimento'];
                                                ?>
                                                <tr class="odd gradeX">
                                                    <td> <?= $movs[$i]['tipo_movimento'] == '0' ? 'Entrada' : 'Saida' ?></td>
                                                    <td> <?= $movs[$i]['valor_movimento'] ?></td>
                                                    <td> <?= $movs[$i]['data_movimento'] ?></td>
                                                    <td> <?= $movs[$i]['nome_categoria'] ?></td>
                                                    <td> <?= $movs[$i]['nome_empresa'] ?></td>
                                                    <td> <?= $movs[$i]['nome_conta'] ?></td>
                                                    <td> <?= $movs[$i]['obs_movimento'] ?></td>
                                                    <td>
                                                        <a href="pesquisar_movimento.php<?= $parametros ?>" class="btn btn-danger btn-sm" ><i class="fa fa-edit"></i> Excluir </a>
                                                    </td>
                                                </tr>                                       
                                            <?php } ?>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>


                    <?php } ?>
                </div>
                <!-- /. PAGE INNER  -->
            </div>
            <!-- /. PAGE WRAPPER  -->
        </div>
    </body>
</html>