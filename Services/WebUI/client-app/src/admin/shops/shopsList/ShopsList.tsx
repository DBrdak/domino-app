import {Skeleton, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography} from "@mui/material";
import LoadingTableRow from "../../../components/LoadingTableRow";
import React from "react";
import {Shop} from "../../../global/models/shop";
import StationaryShopListItem from "./listItems/StationaryShopListItemActions";
import MobileShopListItem from "./listItems/MobileShopListItemActions";
import ShopListItem from "./listItems/ShopListItem";

interface Props {
    shops: Shop[]
    loading: boolean
}

export function ShopsList({shops, loading}: Props) {

    return (
        <TableContainer>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell width={'25%'} style={{textAlign: 'center'}} >
                            <Typography variant='h5' textAlign={'center'}><strong>Nazwa Sklepu</strong></Typography>
                        </TableCell>
                        <TableCell width={'75%'} style={{textAlign: 'center'}}>
                            <Typography variant='h5'><strong>Akcje</strong></Typography>
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {
                        loading ?
                            new Array(4).fill(null).map((_, rowIndex) => (
                                <TableRow key={rowIndex}>
                                    <TableCell style={{ textAlign: 'center', width: `25%` }}>
                                        <Skeleton variant="text" width="80%" height={30} />
                                    </TableCell>
                                    <TableCell style={{ textAlign: 'center', width: `75%` }}>
                                        <Skeleton variant="text" width="80%" height={30} />
                                    </TableCell>
                                </TableRow>
                            ))
                            :
                            shops &&
                            shops.map((p:any) => (
                                <ShopListItem key={p.id} shop={p} />
                            ))
                    }
                </TableBody>
            </Table>
        </TableContainer>
    )
}