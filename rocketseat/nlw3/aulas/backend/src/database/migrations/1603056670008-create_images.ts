import {MigrationInterface, QueryRunner, Table} from "typeorm";

export class createImages1603056670008 implements MigrationInterface {

	public async up(queryRunner: QueryRunner): Promise<void> {
		await queryRunner.createTable(new Table({
			name: 'images',
			columns: [
				{
					name: 'id',
          type: 'integer',
          unsigned: true,
          isPrimary: true,
          isGenerated: true,
          generationStrategy: 'increment',
				},
				{
					name: 'path',
					type: 'varchar',
				},
				{
					name: 'orphanage_id',
					type: 'integer',
				}
			],
			foreignKeys: [
				{
					name: 'ImageOrphanage',
					columnNames: ['orphanage_id'],
					referencedTableName: 'orphanages',
					referencedColumnNames: ['id'],
					onUpdate: '',
					onDelete: '',
				}
			]
		}))
	}

	public async down(queryRunner: QueryRunner): Promise<void> {
		await queryRunner.dropTable('images');
	}

}
