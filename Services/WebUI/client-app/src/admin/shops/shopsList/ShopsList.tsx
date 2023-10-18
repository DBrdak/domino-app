import {Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography} from "@mui/material";
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
                            <LoadingTableRow rows={4} cells={2} />
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