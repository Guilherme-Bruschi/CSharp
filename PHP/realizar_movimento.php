<?php
require_once '../dao/UtilDAO.php';
UtilDAO::VerificarLogado();
require_once '../dao/MovimentoDAO.php';
require_once '../dao/CategoriaDAO.php';
require_once '../dao/EmpresaDAO.php';
require_once '../dao/ContaDAO.php';

if (isset($_POST['btnSalvar'])) {
    $data = $_POST['data'];
    $valor = $_POST['valor'];
    $categoria = $_POST['categoria'];
    $empresa = $_POST['empresa'];
    $conta = $_POST['conta'];
    $tipo = $_POST['tipo'];
    $obs = $_POST['obs'];

    $objdao = new MovimentoDAO();

    $ret = $objdao->LancarMovimento($tipo, $data, $valor, $categoria, $conta, $empresa, $obs);
}

$objdao_cat = new CategoriaDAO();
$cats = $objdao_cat->ConsultarCategoria();
$objdao_emp = new EmpresaDAO();
$emps = $objdao_emp->ConsultarEmpresa();
$objdao_cont = new ContaDAO();
$conts = $objdao_cont->ConsultarConta();
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
                            <h2>Gerenciar Movimentos</h2>   
                            <h5>Realize seus Movimentos aqui!</h5>

                        </div>
                    </div>
                    <!-- /. ROW  -->
                    <hr />
                    <form method="post" action="realizar_movimento.php">

                        <div class="col-md-6">

                            <div class="form-group">
                                <label>Selecionar Movimento</label>
                                <select class="form-control" name="tipo">
                                    <option value="">Selecione</option>
                                    <option value="0">Entrada</option>
                                    <option value="1">Saída</option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label> Data</label>
                                <input type="Date" class="form-control" name="data"></input>                                                                      
                            </div>
                            <div class="form-group">
                                <label>Valor do movimento</label>
                                <input class="form-control" placeholder="Digite aqui" name="valor"/>
                            </div>
                        </div>

                        <div class="col-md-6">

                            <div class="form-group">
                                <label>Categoria</label>
                                <select class="form-control" name="categoria">
                                    <option value="" selected>Selecione</option>
                                    <?php for ($i = 0; $i < count($cats); $i++) { ?>
                                        <option value="<?= $cats[$i]['id_categoria'] ?>">
                                            <?= $cats[$i]['nome_categoria'] ?>
                                        </option>
                                    <?php } ?>
                                </select>
                            </div>
                            <div class="form-group">
                                <label>Empresa</label>
                                <select class="form-control" name="empresa">
                                    <option value="" selected>Selecione</option>
                                    <?php for ($i = 0; $i < count($emps); $i++) { ?>
                                        <option value="<?= $emps [$i]['id_empresa'] ?>">
                                            <?= $emps[$i]['nome_empresa'] ?>
                                        </option>
                                    <?php } ?>
                                </select>
                            </div>
                            <div class="form-group">
                                <label>Conta</label>
                                <select class="form-control" name="conta">
                                    <option value="" selected>Selecione</option>
                                    <?php for ($i = 0; $i < count($conts); $i++) { ?>
                                        <option value="<?= $conts [$i]['id_conta'] ?>">
                                            <?= $conts[$i]['nome_conta'] ?> / Ag : <?= $conts[$i]['agencia_conta'] ?> - Núm: <?= $conts[$i]['numero_conta'] ?> / Saldo: R$ <?= $conts[$i]['saldo_conta'] ?>
                                        </option>
                                    <?php } ?>
                                </select>
                            </div>
                        </div>

                        <div class="col-md-12">

                            <div class="form-group">
                                <label>Observação</label>
                                <textarea class="form-control" rows="3" name="obs"></textarea>
                            </div>

                            <button type="submit" class="btn btn-success" name="btnSalvar">Salvar</button>
                            <button type="submit" class="btn btn-warning">Cancelar</button>

                        </div>
                    </form>
                    <hr />
                </div>
                <!-- /. PAGE INNER  -->

            </div>
            <!-- /. PAGE WRAPPER  -->
        </div>
    </body>
</html>

